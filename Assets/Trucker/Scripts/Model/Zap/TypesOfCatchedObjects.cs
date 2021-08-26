using System;
using System.Collections.Generic;
using System.Linq;
using Trucker.Control.Asteroid;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using UnityEngine;
using UnityUtils.Saves;
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
            AsteroidSparks.OnSparksOn += OnSparksOn;
            AsteroidSparks.OnSparksOff += OnSparksOff;
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

        private void OnSparksOn(ZapCatchee asteroid) 
            => ObjectChangedType(asteroid, EntityType.Asteroid, EntityType.AsteroidWithSparks);

        private void OnSparksOff(ZapCatchee asteroid) 
            => ObjectChangedType(asteroid, EntityType.AsteroidWithSparks, EntityType.Asteroid);

        // IMPR welll that's really bad; should move sparks to subtype level so I wouldn't need to jump around it 
        private void ObjectChangedType(ZapCatchee obj, EntityType from, EntityType to)
        {
            if (!_catcheesByType[from].Contains(obj)) return;
            
            _catcheesByType[from].Remove(obj);
            _catcheesByType[to].Add(obj);
            
            UpdateCatcheesCount();
            
            NotifyOnTypeCountChange(from);
            NotifyOnTypeCountChange(to);
        }
    }
}