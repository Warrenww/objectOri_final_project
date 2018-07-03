using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IObserve
    {
        void Destroy(string ItemType = "BareHand");
        void Construct(string ItemType,GameObject block);
    }
}
