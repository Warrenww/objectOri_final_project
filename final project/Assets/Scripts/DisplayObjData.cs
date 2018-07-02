using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class DisplayObjData : MonoBehaviour
    {
        public Text ObjName;
        public Text ObjDuration;
        public Text ItemName;
        public static DisplayObjData Instance;
        
        void Start()
        { 
            Instance = this;
        }
        public void RenderName(string Name)
        {
            ObjName.text = "Object : " + Name;
        }
        public void RenderDuration(float duration)
        {
            ObjDuration.text = "Duration : " + duration.ToString("N1");
        }
        public void RenderItemName(string Name)
        {
            ItemName.text = "Item : " + Name;
        }
    }
}
