using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Block : MonoBehaviour, IObserve
    {
        protected string BlockName ;
        protected float duration;
        protected float Max_duration;
        protected string efficientTool;

        private Color originalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);   
        public static bool _destroying = false;
        public static bool _constructing = false;
        private bool _focus = false;
        public GameObject selfItem;

        void Start()
        {

        }
        void Update()
        {

        }
        void OnMouseEnter()
        {
            // Highlight block
            GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
            DisplayObjData.Instance.RenderName(BlockName);
            DisplayObjData.Instance.RenderDuration(duration);
            // Subscribe player
            Player.instance.Register(this); 
        }
        void OnMouseExit()
        {
            // Cancel Highlight
            duration = Max_duration;
            GetComponent<Renderer>().material.color = originalColor;
            DisplayObjData.Instance.RenderName("Null");
            DisplayObjData.Instance.RenderDuration(0.0f);
            // Unsubscribe player
            Player.instance.Remove(this);
        }
        private void destroyIt(float damage)
        {
            duration -= damage; // Decrease duration
            // Display how serious the block be damaged
            float percentage = duration / Max_duration * 0.8f;
            float coverColor = 0.8f - percentage;
            GetComponent<Renderer>().material.color = new Color(percentage, percentage, percentage, 1.0f);
            DisplayObjData.Instance.RenderDuration(duration); // Display numerical value
            // Destoy block after duration lower than 0
            if (duration < 0)
            {
                Player.instance.Remove(this); // Unsubscibe player
                Destroy(gameObject);
                Instantiate(selfItem, transform.position, Quaternion.identity);// Turn block into item                
            }
        }
        public void Destroy(string ItemType = "BareHand")
        {
            // Signal from player to damage block
            float damage = 0.1f;
            if (ItemType == efficientTool) damage = 0.5f;
            destroyIt(damage);
        }

        public void Construct(string ItemType,GameObject block)
        {
            Vector3 position; // Position to place block
            Vector3 center = transform.position; // Center of focus block
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // Mouse position
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos); 

            Collider[] hitColliders = Physics.OverlapSphere(center, 1); // Nearby blocks
            List<Vector3> occupied = new List<Vector3>();
            foreach(var c in hitColliders)
            { 
                occupied.Add(c.gameObject.transform.position); // Find nearby blocks' position
            }
            Debug.Log(center);
            foreach(var i in occupied)
            {
                Debug.Log(i);
            }
            // Want construct on up,down,left or right of this block
            List<Vector3> wanted = new List<Vector3>(new Vector3[] {
                center+Vector3.up,center+Vector3.down,center+Vector3.left,center+Vector3.right
            });
            // Remove occupied position
            foreach(var o in occupied)
            {
                if (wanted.IndexOf(o) != -1) wanted.Remove(o);
            }
            if (wanted.Count == 0) return; // If there are no availible position
            // Find out the nearest position to construct
            float min_distance = 1e20f;
            Vector3 nearest = new Vector3();
            foreach(var w in wanted)
            {
                float distance = Vector3.Distance(w, lookPos);
                if(distance < min_distance)
                {
                    min_distance = distance;
                    nearest = w;
                }
            }                
            Instantiate(block, nearest, Quaternion.identity);
            
        }
    }
}
