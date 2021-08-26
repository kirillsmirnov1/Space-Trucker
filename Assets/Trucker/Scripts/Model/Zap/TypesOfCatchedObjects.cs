using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using UnityUtils.Saves;
using UnityEngine;
using UnityUtils.Variables;

namespace Trucker.Model.Zap
{
    [CreateAssetMenu(fileName = "Types Of Catched Objects", menuName = "Data/CatcheeTypes", order = 0)]
    public class TypesOfCatchedObjects : InitiatedScriptableObject
    {
        
        [SerializeField] private IntVariable catcheesTotalCount;
        
        private Dictionary<EntityType, List<ZapCatchee>> _catcheesByType;
        public event Action<EntityType> OnChange;

        public override void Init()
        {
            InitDictionary();
            UpdateCatcheesCount();
        }

        private void InitDictionary()
        {
            _catcheesByType = new Dictionary<EntityType, List<ZapCatchee>>();
            foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
            {
                _catcheesByType.Add(type, new List<ZapCatchee>());
            }
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
            => OnChange?.Invoke(catcheeType);

        public int Count(EntityType[] types) 
            => types.Select(Count).Sum();

        private int Count(EntityType type)
        {
            return _catcheesByType.ContainsKey(type) 
                ? _catcheesByType[type].Count 
                : 0;
        }

        private List<ZapCatchee> GetCatcheesOfType(EntityType type) => _catcheesByType[type];

        public List<ZapCatchee> GetCatcheesOfTypes(EntityType[] types)
        {
            var res = new List<ZapCatchee>();
            foreach (var type in types)
            {
                res.AddRange(GetCatcheesOfType(type));
            }

            return res;
        }
    }
}