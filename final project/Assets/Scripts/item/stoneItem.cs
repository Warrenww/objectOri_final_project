using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.item
{
    class stoneItem : BlockItem
    {
        void Start()
        {
            ItemName = "Stone";
            ItemType = "Block";
        }
    }
}
