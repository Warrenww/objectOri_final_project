using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Item : MonoBehaviour
    {
        protected bool Isfloating = false;
        private int _floating = 0;

        protected float Max_duration;
        protected float duration;
        protected string ItemName;
        protected string ItemType;

        protected Material material;
        private float disappear = 0;

        void Awake()
        {
            material = GetComponent<Renderer>().material;
        }
        void Update()
        {
            // Floating animation
            if (Isfloating)
            {
                if (_floating == 0) transform.position += new Vector3(0, 0.1f, 0);
                else if (_floating == 10)
                {
                    transform.position += new Vector3(0, -0.1f, 0);
                    _floating = 0;
                }
                _floating += 1;
            }
            // Clean up item that doesn't be picked up after 60s.
            disappear += Time.deltaTime;
            if (disappear > 60) Destroy(gameObject);

            // Item is inside the void, continuously take damage
            if (transform.position.y < -50)
            {
                Destroy(gameObject);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                // Picked up by the player
                Player.instance.PickUpItem(this);
                Destroy(gameObject); 
            }
            if (other.gameObject.tag == "block")
            {
                // Stop dropping and float on the block
                Rigidbody rb = GetComponent<Rigidbody>();
                transform.position = other.gameObject.transform.position + new Vector3(0, 1, 0);
                rb.useGravity = false;
                rb.velocity = new Vector3(0, 0, 0);
                Isfloating = true;
            }
        }
        public string displayName()
        {
            return ItemName;
        }
        public Material getMaterial()
        {
            return material;
        }
        public string getItemType()
        {
            return ItemType;
        }
    }
}
