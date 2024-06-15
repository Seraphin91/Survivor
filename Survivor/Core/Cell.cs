using System;

namespace Survivor
{
    public abstract class Cell : IUpdate
    {
        protected Item Content;
        protected int SpawnRange;
        protected int MoveCost;

        protected Cell(int moveCost, int spawnRange)
        {
            Content = null;
            MoveCost = moveCost;
            SpawnRange = spawnRange;
        }

        /*protected Cell()
        {
            throw new NotImplementedException("Delete This Constructor When Game Hard Is Implemented");
        }*/

        public Item GetContent()
        {
            return Content;
        }
        
        public void SetContent(Item item)
        {
            Content = item;
        }
        
        public int GetSpawnRange()
        {
            return SpawnRange;
        }
        
        public int GetMoveCost()
        {
            return MoveCost;
        }
        
        public void SetMoveCost(int cost)
        {
            MoveCost = cost;
        }
        
        public virtual void Update()
        {
            if (Content != null )
            {
                Content.Update();
                if (Content.GetExpiry()<=0)
                {
                    Content = null;
                }
            }
        }
    }
}