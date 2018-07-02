using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IObserve
    {
        void Render(string ItemType = "BareHand");
    }
}
