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
        [Header("Components")]
        [SerializeField] private ZapCatchee catchee;
        
        [Header("Data")]
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;
        [SerializeField] private FloatVariable speed;
        [SerializeField] private FloatVariable scientistAsteroidDistEps;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject warpShotPrefab;
        [SerializeField] private GameObject warpSpherePrefab;
        
        private ZapCatcher ZapCatcher => zapCatcherVariable.Value;
        
        private MadScientistState _state;
        private Transform _asteroidTarget;
        private GameObject _warpShot;
        private GameObject _warpSphere;
        
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
                // TODO enable jetpack
                // TODO show mini-dialogue 
                _scientistTransform = scientist.transform.parent;
                _asteroidTarget = scientist._asteroidTarget;
                scientist.DisconnectFromCatcher();
                scientist.StartCoroutine(FlyToAsteroid());
            }

            private IEnumerator FlyToAsteroid()
            {
                // IMPR move scientist using rb.AddForce
                yield return MoveTo(
                    _scientistTransform,
                    _asteroidTarget,
                    scientist.speed,
                    scientist.scientistAsteroidDistEps);

                scientist.SetInteractionState();
            }
        }

        private class InteractWithAsteroid : MadScientistState
        {
            public override void Start(MadScientist scientistInstance)
            {
                base.Start(scientistInstance);
                PerformWarpShot();
            }

            private void PerformWarpShot()
            {
                // IMPR into init shot 
                var shot = scientist._warpShot ??= Instantiate(scientist.warpShotPrefab);
                var shotsTransform = shot.transform;
                shot.GetComponentInChildren<TrailRenderer>().Clear();
                shotsTransform.position = scientist.transform.position;
                shot.gameObject.SetActive(true);

                scientist.StartCoroutine(MoveWarpShot(shot, shotsTransform));
            }

            private IEnumerator MoveWarpShot(GameObject shot, Transform shotsTransform)
            {
                yield return MoveTo(
                    shotsTransform, 
                    scientist._asteroidTarget, 
                    scientist.speed * 3, 
                    0.1f);

                shot.gameObject.SetActive(false);

                yield return InitiateWarpSphere();
            }

            private IEnumerator InitiateWarpSphere()
            {
                // TODO tick counter 
                var sphere = scientist._warpSphere ??= Instantiate(scientist.warpSpherePrefab);
                var sphereTransform = sphere.transform;
                sphereTransform.parent = scientist._asteroidTarget;
                sphereTransform.localScale = Vector3.zero;
                sphereTransform.localPosition = Vector3.zero;
                sphere.gameObject.SetActive(true);

                while (sphereTransform.localScale.x < 2)
                {
                    sphereTransform.localScale += Vector3.one * 0.05f;
                    yield return null;
                }

                while (scientist._asteroidTarget.localScale.x > 0.001f)
                {
                    scientist._asteroidTarget.localScale -= Vector3.one * 0.15f;
                    yield return null;
                }

                sphereTransform.parent = scientist.transform;
                sphere.gameObject.SetActive(false);

                Destroy(scientist._asteroidTarget.gameObject);
                scientist.DelayAction(1f, () => scientist.SetSearchState());
            }
        }

        private static IEnumerator MoveTo(Transform movable, Transform target, float movementSpeed, float eps)
        {
            Vector3 toTarget;
            do
            {
                toTarget = target.position - movable.position;
                var distToMove = toTarget.normalized * movementSpeed * 3;
                distToMove = Vector3.ClampMagnitude(distToMove, Mathf.Max(toTarget.magnitude, eps));
                movable.Translate(distToMove, Space.World);
                yield return null;
            } while (Mathf.Abs(toTarget.magnitude) > eps);
        }
    }

}