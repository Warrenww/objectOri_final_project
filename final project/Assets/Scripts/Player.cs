using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player: MonoBehaviour
    {
        CharacterController characterController;

        public float smoothRate = 0.01f;
        public float jumpHeight = 1.0f;
        public float HitPoint = 3;

        private float velocity;
        void Start()
        {
            
        }

        void Update()
        {

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position += new Vector3(0.1f, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position += new Vector3(-0.1f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float height = 0;
                height = Mathf.SmoothDamp(gameObject.transform.position.y,jumpHeight, ref velocity, smoothRate);
                gameObject.transform.position += new Vector3(0,height,0); 
            }

           if(transform.position.y < -50)
            {
                transform.position = new Vector3(-49.5f, 20, 0);
            }
            
        }

    }
}
