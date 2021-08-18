using System.Collections;
using Trucker.Control.Zap.Catchee;
using Trucker.Model.Entities;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Asteroid
{
    public class AsteroidSparks : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ParticleSystem sparkParticles;
        [SerializeField] private EntityId entityId;
        [SerializeField] private ZapCatchee zapCatchee;
        
        [Header("Data")]
        [SerializeField] private Vector2Variable sparkDelay;
        [SerializeField] private Vector2Variable sparkDuration;
        
        private static readonly Random Random = new Random();
        private bool _sparkling;
        
        private void Awake()
        {
            zapCatchee.OnCatchingStarted += LockSparks;
            zapCatchee.OnFreed += UnlockSparks;
            StartCoroutine(DelaySparksTurningOn());
        }

        private void OnDestroy()
        {
            zapCatchee.OnCatchingStarted -= LockSparks;
            zapCatchee.OnFreed -= UnlockSparks;
        }

        private void LockSparks() 
            => StopAllCoroutines();

        private void UnlockSparks() 
            => StartCoroutine(_sparkling ? DelaySparksTurningOff() : DelaySparksTurningOn());

        private IEnumerator DelaySparksTurningOn()
        {
            yield return new WaitForSeconds(Random.NextFloat(sparkDelay.Value.x, sparkDelay.Value.y)); // IMPR x/y property to v2var
            TurnSparksOn();
            yield return DelaySparksTurningOff();
        }

        private IEnumerator DelaySparksTurningOff()
        {
            yield return new WaitForSeconds(Random.NextFloat(sparkDuration.Value.x, sparkDuration.Value.y));
            TurnSparksOff();
            yield return DelaySparksTurningOn();
        }

        private void TurnSparksOn()
        {
            sparkParticles.Play();
            _sparkling = true;
            entityId.type = EntityType.AsteroidWithSparks;
        }

        private void TurnSparksOff()
        {
            sparkParticles.Stop();
            _sparkling = false;
            entityId.type = EntityType.Asteroid;
        }
    }
}
