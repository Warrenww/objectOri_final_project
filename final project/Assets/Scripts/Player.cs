using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player: MonoBehaviour
    {
        public static Player instance;
        public Rigidbody rb;

        public Texture health_0;
        public Texture health_05;
        public Texture health_1;

        private int HitPoint;
        private int Max_HP = 20;
        private float jumpHeight = 5.0f;
        private float walkSpeed = 5.0f;
        private float startJumpHeight;
        void Start()
        {
            instance = this;
            rb = GetComponent<Rigidbody>();
            HitPoint = Max_HP;
        }
        void Update()
        {
            // Player is inside the void, continuously take damage
            if (transform.position.y < -50)
            {
                takeDamage(1);
            }
            // Die
            if( HitPoint < 0)
            {
                transform.position = new Vector3(-49.5f, 20, 0);
                rb.velocity = new Vector3(0, 0, 0);
                HitPoint = Max_HP;
            }
        }

        void FixedUpdate()
        {
            Vector3 originalVelocity = rb.velocity;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector3(walkSpeed, originalVelocity.y, originalVelocity.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(-walkSpeed, originalVelocity.y, originalVelocity.z);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((int)originalVelocity.y == 0)
                {
                    startJumpHeight = transform.position.y; // Reserve height value when starting jump
                    rb.velocity = new Vector3(originalVelocity.x, jumpHeight, originalVelocity.z);
                }
            }
            
        }
        void OnGUI()
        {
            float currentHP = HitPoint;
            for(int i = 0; i < 10; i++)
            {
                if(currentHP>1)
                GUI.DrawTexture(new Rect(10 + (i * 16), 10, 16, 16), health_1, ScaleMode.ScaleToFit, true, 0);
                else if (currentHP>0)
                GUI.DrawTexture(new Rect(10 + (i * 16), 10, 16, 16), health_05, ScaleMode.ScaleToFit, true, 0);
                else
                GUI.DrawTexture(new Rect(10 + (i * 16), 10, 16, 16), health_0, ScaleMode.ScaleToFit, true, 0);
                currentHP -= 2;

            }
        }
        void OnCollisionEnter(Collision other)
        {
            // Hit on block
            if(other.gameObject.tag == "block")
            {
                // Calculate height different
                float currentHeight = transform.position.y;
                float deltaHeight = startJumpHeight/2 - currentHeight/2;
                if (deltaHeight > 0)
                {
                    takeDamage((int)deltaHeight); // Taking damage
                }
                startJumpHeight = currentHeight; // Reset jumpHeight to prevent continuously take damage
            }
        }
        public void takeDamage(int damage)
        {
            HitPoint -= damage;
        }

    }
}
