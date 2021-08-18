using System.Collections;
using UnityEngine;
using UnityUtils;
using UnityUtils.Variables;
using Random = System.Random;

namespace Trucker.Control.Asteroid
{
    public class AsteroidSparks : MonoBehaviour
    {
        [SerializeField] private ParticleSystem sparkParticles;
        [SerializeField] private Vector2Variable sparkDelay;
        [SerializeField] private Vector2Variable sparkDuration;
        
        private static readonly Random Random = new Random();
        private bool _sparkling;
        
        private void Awake()
        {
            StartCoroutine(DelaySparksTurningOn());
        }

        private IEnumerator DelaySparksTurningOn()
        {
            yield return new WaitForSeconds(Random.NextFloat(sparkDelay.Value.x, sparkDelay.Value.y)); // IMPR x/y property to v2var
            sparkParticles.Play();
            _sparkling = true;
            yield return DelaySparksTurningOff();
        }

        private IEnumerator DelaySparksTurningOff()
        {
            yield return new WaitForSeconds(Random.NextFloat(sparkDuration.Value.x, sparkDuration.Value.y));
            sparkParticles.Stop();
            _sparkling = false;
            yield return DelaySparksTurningOn();
        }
        
        // TODO catchStart locks sparks
        // TODO catchRelease releases 
        // TODO change type to AsteroidWith Sparks 
    }
}
