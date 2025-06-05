using NF.TD.Enemy.Core;
using NF.TD.Interfaces;
using UniRx.Examples;
using UnityEngine;

namespace NF.TD.Bullet
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        Vector3 velocity;

        private float speed;
        private int damageAmount;

        void Awake()
        {
            // Attempt to get the Rigidbody component attached to this GameObject
            TryGetComponent(out rb);
        }

        void Start()
        {
            // Calculate the velocity based on the object's forward direction and speed
            velocity = transform.forward * speed;

            Destroy(gameObject, 2f);
        }

        void FixedUpdate()
        {
            var displacement = velocity * Time.deltaTime;

            //For smooth movement
            rb.MovePosition(rb.position + displacement);
        }

        //For now this will be used for testing purposes
        void OnCollisionEnter(Collision other)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }

        public void Initialize(float speed, int damage)
        {
            this.speed = speed;
            this.damageAmount = damage;
        }
    }
}
