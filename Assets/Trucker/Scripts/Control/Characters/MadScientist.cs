using System.Collections;
using Trucker.Control.Zap;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;
using UnityUtils.Extensions;
using UnityUtils.Variables;

namespace Trucker.Control.Characters
{
    [RequireComponent(typeof(SphereCollider))]
    public class MadScientist : MonoBehaviour // I'm really free-writing it now
    {
        [SerializeField] private ZapCatchee catchee;
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;
        [SerializeField] private FloatVariable speed;
        
        private ZapCatcher ZapCatcher => zapCatcherVariable.Value;
        
        private MadScientistState _state;
        private Transform _asteroidTarget;

        private void OnValidate() => this.CheckNullFields();
        private void Start() => SetSearchState();
        private void SetSearchState() => SetState(new SparklingAsteroidSearch());
        private void SetFlyingToTargetState() => SetState(new FlyingToTarget());
        private void SetInteractionState() => SetState(new InteractWithAsteroid());
        private void SetState(MadScientistState newState)
        {
            _state?.Stop();
            _state = newState;
            newState.Start(this);
        }

        private void OnTriggerEnter(Collider other) => _state.OnTriggerEnter(other);

        private void ConnectToCatcher() => ZapCatcher.TryCatch(catchee);
        private void DisconnectFromCatcher() => ZapCatcher.TryFree(catchee);

        private abstract class MadScientistState
        {
            protected MadScientist scientist;
            
            public virtual void Start(MadScientist scientistInstance)
            {
                scientist = scientistInstance;
                Debug.Log($"MadScientistState: {GetType().Name}");
            }

            public virtual void Stop(){}
            public virtual void OnTriggerEnter(Collider other) { }
        }

        private class SparklingAsteroidSearch : MadScientistState // IMPR maybe add trigger sphere on player? 
        {
            public override void Start(MadScientist scientistInstance)
            {
                base.Start(scientistInstance);
                scientist.ConnectToCatcher();
            }

            public override void OnTriggerEnter(Collider other)
            {
                base.OnTriggerEnter(other);
                if (other.TryGetComponent<EntityId>(out var id) && id.type == EntityType.AsteroidWithSparks)
                {
                    scientist._asteroidTarget = other.transform;
                    scientist.SetFlyingToTargetState();
                }
            }
        }

        private class FlyingToTarget : MadScientistState
        {
            private Transform _scientistTransform;
            private Transform _asteroidTarget;
            
            public override void Start(MadScientist scientistInstance)
            {
                base.Start(scientistInstance);
                _scientistTransform = scientist.transform.parent;
                _asteroidTarget = scientist._asteroidTarget;
                scientist.DisconnectFromCatcher();
                scientist.StartCoroutine(FlyToAsteroid());
            }

            private IEnumerator FlyToAsteroid()
            {
                Vector3 toAsteroid;
                do
                {
                    toAsteroid = _asteroidTarget.position - _scientistTransform.position;
                    var distToMove = toAsteroid.normalized * scientist.speed;
                    _scientistTransform.Translate(distToMove, Space.World); // IMPR use rb.AddForce 
                    yield return null;
                } while (Mathf.Abs(toAsteroid.magnitude) > 5f);
                scientist.SetInteractionState();
            }
        }

        private class InteractWithAsteroid : MadScientistState
        {
            // TODO shoot warp sphere, wait for it destruction, connect to ZapCatcher
            public override void Start(MadScientist scientistInstance)
            {
                base.Start(scientistInstance);
                Debug.Log("Should be shooting rn");
                Destroy(scientist._asteroidTarget.gameObject);
                scientist.DelayAction(1f, () => scientist.SetSearchState());
            }
        }
    }

}