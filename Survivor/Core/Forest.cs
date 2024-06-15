using System;

namespace Survivor
{
    public class Forest : Cell
    {
        public Forest(int moveCost, int spawnRange):base(moveCost,spawnRange)
        {
            
        }
        
        public override string ToString()
        {
            return (Content != null ? Content.ToString() : " ");
        }

    }
}


