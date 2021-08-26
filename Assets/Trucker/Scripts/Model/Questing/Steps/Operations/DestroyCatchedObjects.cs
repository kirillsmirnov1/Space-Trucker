﻿using System;
using Trucker.Control.Zap;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;

namespace Trucker.Model.Questing.Steps.Operations
{
    [CreateAssetMenu(fileName = "Consequence_DestroyCatchedObjects", menuName = "Quests/Steps/Destroy Catched", order = 0)]
    public class DestroyCatchedObjects : Operation
    {
        public static event Action<EntityType, int> OnObjectsDestroyed; 
        
        [SerializeField] private EntityType[] typesToDestroy;
        [SerializeField] private int numberOfObjectsToDestroy;
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;

        private ZapCatcher Catcher => zapCatcherVariable.Value;
        
        public override void Start()
        {

            var catchees = Catcher.TryFree(typesToDestroy, numberOfObjectsToDestroy);
            foreach (var catchee in catchees)
            {
                Destroy(catchee.gameObject);
            }
            // OnObjectsDestroyed?.Invoke(typesToDestroy, numberOfObjectsToDestroy);
        }
    }
}