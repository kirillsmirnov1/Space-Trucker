﻿using System.Linq;
using UnityEngine;
using UnityUtils.Saves;
using UnityUtils.Variables;

namespace Trucker.Model.Beacons
{
    [CreateAssetMenu(menuName = "Logic/NearestBeaconAnchor", fileName = "NearestBeaconAnchor", order = 0)]
    public class NearestBeaconAnchor : InitiatedScriptableObject // IMPR that's actually beacon's anchors data 
    {
        [SerializeField] private TransformVariable playersTransformVariable;
        [SerializeField] private Vector3Variable[] anchorPositions; // IMPR into V3ArrayVariable 

        private Transform _player;
        private Vector3[] _anchors;
        private bool[] _anchorLocked;
        
        private static readonly object Lock = new object();
        
        public override void Init()
        {
            _player = playersTransformVariable;
            _anchors = anchorPositions.Select(anchorPosVariable => anchorPosVariable.Value).ToArray();
            _anchorLocked = new bool[_anchors.Length];
        }

        public Vector3 NearestAnchor()
            => PickNearestAnchorFrom(_anchors);

        private Vector3 PickNearestAnchorFrom(Vector3[] anchors)
        {
            var playerPos = _player.position;
            var bestDistance = float.MaxValue;
            var nearestAnchor = Vector3.zero;
            
            for (int i = 0; i < anchors.Length; i++)
            {
                if(_anchorLocked[i]) continue;
                var anchor = anchors[i];
                var distance = Vector3.Distance(playerPos, anchor); 
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    nearestAnchor = anchor;
                }
            }

            return nearestAnchor;
        }

        public bool TryLock(Vector3 anchorPos)
        {
            lock (Lock)
            {
                var index = FindAnchorIndex(anchorPos);

                if (index == -1) return false;
                
                if (_anchorLocked[index]) return false;
                
                _anchorLocked[index] = true;
                return true;
            }
        }

        public void Unlock(Vector3 anchorPos)
        {
            lock (Lock)
            {
                var index = FindAnchorIndex(anchorPos);
                if (index == -1) return;
                _anchorLocked[index] = false;
            }
        }

        private int FindAnchorIndex(Vector3 anchorPos)
        {
            var index = -1;
            
            for (int i = 0; i < _anchors.Length; i++)
            {
                if (anchorPos.Equals(_anchors[i]))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}