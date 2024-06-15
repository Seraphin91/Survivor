using System;

namespace Survivor
{
    public class River : Cell
    {
        public River(int moveCost, int spawnRange):base(moveCost,spawnRange)
        {
           
        }

        public override string ToString()
        {
            return (Content != null ? Content.ToString() : " ");
        }

    }
}