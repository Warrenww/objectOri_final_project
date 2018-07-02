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
                Instantiate(selfItem, transform.position, Quaternion.identity); // Turn block into item
            }
        }
        public void Render(string ItemType = "BareHand")
        {
            // Signal from player to damage block
            float damage = 0.1f;
            if (ItemType == efficientTool) damage = 0.5f;
            destroyIt(damage);
        }
    }
}
