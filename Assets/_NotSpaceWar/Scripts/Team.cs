using System;
using UnityEngine;

namespace Com.AdrienHeisch.NotSpaceWar
{
    public struct Team
    {
        
        private static byte nextMask = 0;

        [NonSerialized] public byte mask;
        public Color color;

        public Team (Color color)
        {
            mask = nextMask++;
            this.color = color;
        }

    }
}
