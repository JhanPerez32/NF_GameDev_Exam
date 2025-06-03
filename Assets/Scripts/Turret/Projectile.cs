using UnityEngine;

namespace NF.TD.Bullet
{
    public class Projectile : MonoBehaviour
    {
        public float speed;

        Rigidbody rb;
        Vector3 velocity;

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
        void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}
