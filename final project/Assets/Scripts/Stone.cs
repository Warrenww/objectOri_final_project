using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Stone : Block
    {
        void Start()
        {
            Max_duration = 20.0f;
            BlockName = "stone";
            duration = Max_duration;
        }
    }
}
