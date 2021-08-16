using UnityEngine;
using UnityUtils;
using Random = System.Random;

namespace Trucker.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class JunkRandomMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector2 speedModificator = new Vector2(50f, 100f);

        private float _speedModificator;
        private Vector3 _direction;
        private static readonly Random Random = new Random();

        private void Awake()
        {
            InitSpeedModificator();
            RandomizeDirection();
            transform.forward = _direction;
            SetVelocity();
        }

        private void InitSpeedModificator()
        {
            _speedModificator = Random.NextFloat(speedModificator.x, speedModificator.y);
        }

        private void FixedUpdate()
        {
            SetVelocity();
        }

        private void SetVelocity()
        {
            rb.velocity = _direction * (_speedModificator * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other) 
        {
            ReflectDirection(other.GetContact(0).normal);
        }
        
        private void ReflectDirection(Vector3 contactNormal)
        {
            _direction = Vector3.Reflect(_direction, contactNormal);
        }

        private void RandomizeDirection()
        {
            _direction = Random.NextVector(-Vector3.one, Vector3.one);
        }
    }
}