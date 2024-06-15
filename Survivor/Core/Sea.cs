using System;

namespace Survivor
{
    public class Sea : Cell
    {
        public Sea(int moveCost, int spawnRange):base(moveCost,spawnRange)
        {
            
        }

        public override string ToString()
        { 
            return (Content != null ? Content.ToString() : " ");
        }

    }
}