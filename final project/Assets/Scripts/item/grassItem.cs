using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.item
{
    class grassItem : BlockItem
    {
        void Start()
        {
            ItemName = "Grass";
            ItemType = "Block";
        }
    }
}
