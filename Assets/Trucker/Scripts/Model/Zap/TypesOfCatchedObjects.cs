using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Zap
{
    [CreateAssetMenu(fileName = "Types Of Catched Objects", menuName = "Data/CatcheeTypes", order = 0)]
    public class TypesOfCatchedObjects : ScriptableObject
    {
        
        [SerializeField] private IntVariable catcheesTotalCount;
        
        private Dictionary<EntityType, List<ZapCatchee>> _catcheesByType;
        public event Action<EntityType, int> OnChange;

        public void Init()
        {
            _catcheesByType = new Dictionary<EntityType, List<ZapCatchee>>();
            UpdateCatcheesCount();
        }
        
        private void UpdateCatcheesCount()
        {
            catcheesTotalCount.Value = _catcheesByType
                .Select(kv => kv.Value.Count)
                .Sum();
        }

        public void ObjectCatched(ZapCatchee zapCatchee)
        {
            var catcheeType = zapCatchee.Type;
            if (!_catcheesByType.ContainsKey(catcheeType))
            {
                _catcheesByType.Add(catcheeType, new List<ZapCatchee>());
            }
            _catcheesByType[catcheeType].Add(zapCatchee);
            UpdateCatcheesCount();
            NotifyOnTypeCountChange(catcheeType);
        }

        public void ObjectReleased(ZapCatchee zapCatchee)
        {
            var catcheeType = zapCatchee.Type;
            _catcheesByType[catcheeType].Remove(zapCatchee);
            UpdateCatcheesCount();
            NotifyOnTypeCountChange(catcheeType);
        }

        private void NotifyOnTypeCountChange(EntityType catcheeType) 
            => OnChange?.Invoke(catcheeType, _catcheesByType[catcheeType].Count);

        public int Count(EntityType type)
        {
            return _catcheesByType.ContainsKey(type) 
                ? _catcheesByType[type].Count 
                : 0;
        }

        public ZapCatchee GetCatcheeOfType(EntityType type) 
            => _catcheesByType[type][0];
    }
}