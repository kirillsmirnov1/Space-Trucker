using System;
using System.Collections;
using System.Linq;
using Trucker.Control.Asteroid;
using Trucker.Control.Zap;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Dialogues;
using Trucker.Model.Entities;
using Trucker.Model.Questing.Steps.Operations;
using Trucker.Model.Zap;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;

namespace Trucker.Control.Characters
{
    [RequireComponent(typeof(SphereCollider))]
    public class MadScientist : MonoBehaviour // I'm really free-writing it now // That should be SO, or attached in runtime 
    {
        [Header("Components")]
        [SerializeField] private ZapCatchee catchee;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject jetpackTrails;
        
        [Header("Data")]
        [SerializeField] private FloatVariable speed;
        [SerializeField] private FloatVariable rotationSpeed;
        [SerializeField] private FloatVariable scientistAsteroidDistEps;
        [SerializeField] private ZapCatcherVariable zapCatcherVariable;
        [SerializeField] private TransformVariable playerTransform;
        [SerializeField] private Oneliners asteroidFoundLines;
        [SerializeField] private MiniDialogueStep miniDialogueInvoker;
        
        [Header("Variables")]
        [SerializeField] private IntVariable warpedAsteroidsCounter;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject warpShotPrefab;
        [SerializeField] private GameObject warpSpherePrefab;
        
        private ZapCatcher ZapCatcher => zapCatcherVariable.Value;
        
        private MadScientistState _state;
        private Transform _asteroidTarget;
        private GameObject _warpShot;
        private TrailRenderer _warpShotTrail;
        private GameObject _warpSphere;
        
        private void OnValidate() => this.CheckNullFields();
        private void Start() => SetSearchState();
        private void SetSearchState() => SetState(new SparklingAsteroidSearch(this));
        private void SetState(MadScientistState newState)
        {
            _state?.Stop();
            _state = newState;
            newState.Start();
        }

        private void OnTriggerEnter(Collider other) => _state.OnTriggerEnter(other);

        private void ConnectToCatcher() => ZapCatcher.TryCatch(catchee);
        private void DisconnectFromCatcher() => ZapCatcher.TryFree(catchee);
        private void FlyToPlayer() 
            => SetState(new FlyingToTarget(this, playerTransform.Value, new SparklingAsteroidSearch(this)));
        private void FlyToAsteroid()
            => SetState(new FlyingToTarget(this, _asteroidTarget, new InteractWithAsteroid(this)));
        
        private void MoveForward(Vector3 force) 
            => rb.AddForce(transform.forward * force.magnitude, ForceMode.Acceleration); 

        private abstract class MadScientistState
        {
            protected readonly MadScientist scientist;

            protected MadScientistState(MadScientist scientistInstance)
            {
                scientist = scientistInstance;
            }
            public virtual void Start(){}
            public virtual void Stop(){}
            public virtual void OnTriggerEnter(Collider other) { }
        }

        private class SparklingAsteroidSearch : MadScientistState // IMPR maybe add trigger sphere on player? 
        {
            private Coroutine _rotationCoroutine;
            private Transform _scientistTransform;
            private Transform _playerTransform;
            
            public SparklingAsteroidSearch(MadScientist scientistInstance) : base(scientistInstance) { }

            public override void Start()
            {
                base.Start();
                scientist.jetpackTrails.gameObject.SetActive(false);
                scientist.ConnectToCatcher();
                AlignWithPlayer();
            }

            private void AlignWithPlayer()
            {
                _scientistTransform = scientist.transform.parent;
                _playerTransform = scientist.playerTransform;
                _rotationCoroutine = scientist.StartCoroutine(PlayerAlignment());
            }

            public override void Stop()
            {
                base.Stop();
                scientist.DisconnectFromCatcher();
                scientist.StopCoroutine(_rotationCoroutine);
            }

            private IEnumerator PlayerAlignment()
            {
                while (true)
                {
                    _scientistTransform.rotation = Quaternion.Slerp(_scientistTransform.rotation, _playerTransform.rotation, 0.01f);
                    yield return null;
                }
            }

            public override void OnTriggerEnter(Collider other)
            {
                base.OnTriggerEnter(other);
                if (other.TryGetComponent<EntityId>(out var id) && id.type == EntityType.AsteroidWithSparks)
                {
                    scientist._asteroidTarget = other.transform;
                    other.GetComponent<ZapCatchee>().SetUnavailableState();
                    other.GetComponent<AsteroidSparks>().LockSparks();
                    ShowMiniDialogueOneliner();
                    scientist.FlyToAsteroid();
                }
            }

            private void ShowMiniDialogueOneliner()
            {
                var line = scientist.asteroidFoundLines.Entries.Shuffle().First();
                scientist.miniDialogueInvoker.Invoke(line);
            }
        }

        private class FlyingToTarget : MadScientistState
        {
            private readonly Transform _scientistTransform;
            private readonly Transform _target;
            private readonly MadScientistState _nextState;
            private Coroutine _rotationCoroutine;

            public FlyingToTarget(MadScientist scientist, Transform targetTransformInstance, MadScientistState nextState) : base(scientist)
            {
                _target = targetTransformInstance;
                _nextState = nextState;
                _scientistTransform = scientist.transform.parent;
            }
            
            public override void Start()
            {
                base.Start();
                scientist.jetpackTrails.gameObject.SetActive(true);
                scientist.StartCoroutine(Movement());
            }

            private IEnumerator Movement()
            {
                _rotationCoroutine = scientist.StartCoroutine(Rotation());
                yield return FlyToTarget();
                scientist.SetState(_nextState);
            }

            private IEnumerator Rotation()
            {
                while (true)
                {
                    var lookRotation = Quaternion.LookRotation(_target.position - _scientistTransform.position);
                    _scientistTransform.rotation = Quaternion.Slerp(_scientistTransform.rotation,lookRotation,scientist.rotationSpeed);
                    yield return null;
                }
            }

            private IEnumerator FlyToTarget()
            {
                yield return MoveTo(
                    _scientistTransform,
                    _target,
                    scientist.speed,
                    scientist.scientistAsteroidDistEps,
                    scientist.MoveForward);
            }

            public override void Stop()
            {
                base.Stop();
                scientist.StopCoroutine(_rotationCoroutine);
            }
        }
        
        private class InteractWithAsteroid : MadScientistState
        {
            public InteractWithAsteroid(MadScientist scientistInstance) : base(scientistInstance) { }

            public override void Start()
            {
                base.Start();
                scientist.StartCoroutine(InteractionWithAsteroid());
            }

            private IEnumerator InteractionWithAsteroid()
            {
                var shot = InitWarpShot();
                yield return MoveWarpShot(shot.transform);
                DisableWarpShot(shot);
                var warpSphere = InitWarpSphere(out var warpSphereTransform);
                yield return WarpSphereScale(warpSphereTransform);
                DisableWarpSphere(warpSphere, warpSphereTransform);
                DestroyTargetAsteroid();
                IterateWarpedAsteroidsCounter();
                FlyToPlayer();
            }

            private GameObject InitWarpShot()
            {
                var shot = scientist._warpShot ??= Instantiate(scientist.warpShotPrefab);
                var shotTrail = scientist._warpShotTrail ??= shot.GetComponentInChildren<TrailRenderer>();
                shotTrail.Clear(); 
                shot.transform.position = scientist.transform.position;
                shot.gameObject.SetActive(true);
                return shot;
            }

            private IEnumerator MoveWarpShot(Transform shotsTransform)
            {
                yield return MoveTo(
                    shotsTransform, 
                    scientist._asteroidTarget, 
                    scientist.speed * 3, 
                    0.1f,
                    direction => shotsTransform.Translate(direction, Space.World));
            }

            private static void DisableWarpShot(GameObject shot) => shot.gameObject.SetActive(false);

            private GameObject InitWarpSphere(out Transform sphereTransform)
            {
                var sphere = scientist._warpSphere ??= Instantiate(scientist.warpSpherePrefab);
                sphereTransform = sphere.transform;
                sphereTransform.parent = scientist._asteroidTarget;
                sphereTransform.localScale = Vector3.zero;
                sphereTransform.localPosition = Vector3.zero;
                sphere.gameObject.SetActive(true);
                return sphere;
            }

            private IEnumerator WarpSphereScale(Transform sphereTransform)
            {
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
            }

            private void DisableWarpSphere(GameObject warpSphere, Transform sphereTransform)
            {
                sphereTransform.parent = scientist.transform;
                warpSphere.gameObject.SetActive(false);
            }

            private void DestroyTargetAsteroid() 
                => Destroy(scientist._asteroidTarget.gameObject);

            private void IterateWarpedAsteroidsCounter() 
                => scientist.warpedAsteroidsCounter.Value++;

            private void FlyToPlayer() 
                => scientist.FlyToPlayer();
        }

        private static IEnumerator MoveTo(Transform movable, Transform target, float movementSpeed, float eps, Action<Vector3> moveAction)
        {
            Vector3 toTarget;
            do
            {
                toTarget = target.position - movable.position;
                var distToMove = toTarget.normalized * movementSpeed * 3;
                distToMove = Vector3.ClampMagnitude(distToMove, Mathf.Max(toTarget.magnitude, eps));
                moveAction.Invoke(distToMove);
                yield return null;
            } while (Mathf.Abs(toTarget.magnitude) > eps);
        }
    }

}