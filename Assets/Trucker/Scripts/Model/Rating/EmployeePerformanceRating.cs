﻿using System;
using System.Linq;
using OneLine;
using Trucker.Model.NPC;
using UnityEngine;
using UnityUtils.Saves;

namespace Trucker.Model.Rating
{
    [CreateAssetMenu(menuName = "Logic/EmployeePerformanceRating", fileName = "EmployeePerformanceRating", order = 0)]
    public class EmployeePerformanceRating : InitiatedScriptableObject
    {
        public static event Action<int> OnRatingChange;

        [SerializeField, OneLine, HideLabel] private TaskReward[] rewards;

        private int _rating;
        public int Rating => _rating;

        public override void Init()
        {
            SubscribeToTriggers();
            UpdateRating(true);
        }

        private void SubscribeToTriggers()
        {
            foreach (var reward in rewards)
            {
                reward.trigger.OnChange += OnTriggerChange;
            }
        }

        private void OnTriggerChange(bool obj) 
            => UpdateRating();

        private void UpdateRating(bool init = false)
        {
            var oldRating = _rating;

            _rating = rewards
                .Where(reward => reward.trigger.Value)
                .Sum(reward => reward.ratingChange);

            if(!init) OnRatingChange?.Invoke(_rating - oldRating);
        }

        [Serializable]
        public struct TaskReward
        {
            public Dialogue trigger;
            public int ratingChange;
        }
    }
}