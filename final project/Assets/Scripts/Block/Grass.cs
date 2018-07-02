using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Grass : Block
    {
        void Start()
        {
            Max_duration = 10.0f;
            BlockName = "grass";
            duration = Max_duration;
            efficientTool = "Shovel";
        }

    }
}
