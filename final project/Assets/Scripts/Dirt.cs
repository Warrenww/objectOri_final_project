using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Dirt : Block
    {
        void Start()
        {
            BlockName = "dirt";
            Max_duration = 10.0f;
            duration = Max_duration;
        }
    }
}
