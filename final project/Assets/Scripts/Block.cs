using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Block : MonoBehaviour
    {
        //public GUI displayGUI;

        protected string BlockName ;
        protected float duration;
        protected float Max_duration;

        private Color originalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);   
        private static bool _destroying = false;
        private bool _focus = false;

        void Start()
        {

        }

        void Update()
        {
            if (_destroying && _focus)
            {
                destroyIt();
            }
        }
        void OnMouseEnter()
        {
            GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
            DisplayObjData.Instance.RenderName(BlockName);
            DisplayObjData.Instance.RenderDuration(duration);
            _focus = true;
        }
        void OnMouseExit()
        {
            GetComponent<Renderer>().material.color = originalColor;
            DisplayObjData.Instance.RenderName("");
            DisplayObjData.Instance.RenderDuration(0.0f);
            _focus = false;
        }

        void OnMouseDown()
        {
            _destroying = true;
        }
        void OnMouseUp()
        {
            _destroying = false;
            duration = Max_duration;
            GetComponent<Renderer>().material.color = originalColor;
            DisplayObjData.Instance.RenderDuration(duration);

        }

        private void destroyIt()
        {
            duration -= 0.1f;
            float percentage = duration / Max_duration * 0.8f;
            float coverColor = 0.8f - percentage;
            GetComponent<Renderer>().material.color = new Color(percentage, percentage, percentage, 1.0f);
            DisplayObjData.Instance.RenderDuration(duration);
            if (duration < 0)
            {
                Destroy(gameObject);
                _destroying = false;
            }
        }
    }
}
