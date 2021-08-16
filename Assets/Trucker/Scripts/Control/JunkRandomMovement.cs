using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUtils;
using Random = System.Random;

namespace Trucker.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class JunkRandomMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector2 forceFromTo = new Vector2(1f, 5f);

        private float _forceMod;
        private readonly Random _random = new Random();
        private Vector3 _direction;

        private void Awake()
        {
            _forceMod = _random.NextFloat(forceFromTo.x, forceFromTo.y);
            ChangeDirection();
            transform.forward = _direction;
        }

        private void FixedUpdate()
        {
            var force = transform.forward * (Time.deltaTime * _forceMod);
            rb.AddForce(force);
        }

        private void OnCollisionEnter(Collision other) // FIXME not working as intended 
        {
            ChangeDirection(); 
        }

        private void ChangeDirection() // FIXME not random 
        {
            _direction = _random.NextVector(-Vector3.one, Vector3.one);
        }
    }
}