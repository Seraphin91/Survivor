using System;

namespace Survivor
{
    public class PlayerAdvanced : PlayerIntermediate
    {
        public PlayerAdvanced(int maxEnergy, int x, int y):base(maxEnergy,x,y)
        {
        }
        
        public PlayerAdvanced()
        {
            throw new NotImplementedException("Delete This Constructor When Player Hard Is Implemented");
        }
        
        public void Plant(Cell cell)
        {
            Item a = cell.GetContent();
            if (a is Banana or Coconut or Plum)
            {
                Tree arbre = new Tree();
                cell.SetContent(arbre);
            }
        }
    }
}
