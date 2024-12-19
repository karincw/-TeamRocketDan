using Karin;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JSY
{
    public class MochiMove : MonoSingleton<MochiMove>
    {
        private List<Mochi> mochiList = new List<Mochi>();
        
        public void UpdatePosition(Transform trm)
        {
            mochiList = GetComponentsInChildren<Mochi>().ToList();

            foreach (Mochi mochi in mochiList)
            {
                mochi.transform.position = trm.position;
            }
        }
    }
}