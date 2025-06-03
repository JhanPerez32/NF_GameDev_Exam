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
        
        //Destroy GameObject if didn't hit anything for set seconds
        private void Update()
        {
            Destroy(gameObject, 2f);
        }

        void Start()
        {
            // Calculate the velocity based on the object's forward direction and speed
            velocity = transform.forward * speed;
        }

        void FixedUpdate()
        {
            var displacement = velocity * Time.deltaTime;

            //For smooth movement
            rb.MovePosition(rb.position + displacement);
        }

        //For now this will be used for testing purposes
        /*void OnCollisionEnter(Collision other)
        {
            GameObject hitEffect = Instantiate(hitPrefab, other.GetContact(0).point, Quaternion.identity);
            Destroy(hitEffect, 1.5f);

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }

            Destroy(gameObject);

        //TODO: Create a Simple Enemy Health Script in order to properly test this
        }*/

        public void Initialize(float speed, int damage)
        {
            this.speed = speed;
            this.damageAmount = damage;
        }
    }
}
