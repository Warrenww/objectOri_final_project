using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IObservable
    {
        void Register(IObserve o);
        void Remove(IObserve o);
        void Notify();
    }
}
