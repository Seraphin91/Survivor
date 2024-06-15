using System;
namespace Survivor
{
    public class PlayerIntermediate : Player
    {
        protected Item[] Inventory; 
        protected readonly int SizeInventory;
        
        public PlayerIntermediate(int maxEnergy, int x, int y):base(maxEnergy,x,y)
        {
            SizeInventory = 10;
            Inventory = new Item [SizeInventory];
        }
        
        public PlayerIntermediate()
        {
            throw new NotImplementedException("Delete This Constructor When Game Hard Is Implemented");
        }
        
        public void SetEnergy(int energy)
        {
            Energy += energy;
        }

        public Item[] GetInventory()
        {
            return Inventory;
        }
 
        private static int SelectItem()
        {
            char touch;
            do
            {
                Console.WriteLine("Choose a slot to select (0-9) or 'q' to cancel.");
                do
                {
                    touch = (char)Console.Read();
                } while (touch == 0x000d || touch == 0x000a);
            } while ((touch < 48 || touch > 57) && touch != 'q');

            if (touch == 'q')
            {
                return -1;
            }
            else
            {
                return touch - '0';
            }
        }
        
        public void PickUp(Cell[,] board)
        {
            if (board[Coordinates.x,Coordinates.y] == null)
            {
                throw new ArgumentNullException("The cell is empty.");
            }

            for (int i = 0; i < SizeInventory && board[Coordinates.x,Coordinates.y] != null; i++)
            {
                if (Inventory[i] == null)
                {
                    Inventory[i] = board[Coordinates.x, Coordinates.y].GetContent();
                    board[Coordinates.x, Coordinates.y].SetContent(null);

                }
            }
        }

        public void Drop(Cell[,] board)
        {
            int i = SelectItem();
            if (i>=0 && Inventory[i] != null && board[Coordinates.x,Coordinates.y].GetContent() == null)
            {
                board[Coordinates.x,Coordinates.y].SetContent(Inventory[i]);
                Inventory[i] = null;
            }
        }
        
        public void Sacrifice(Game game, God god)
        {
            int i = SelectItem();
            if (i>=0 && Inventory[i] != null)
            {
                god.ReceiveOffering(game,Inventory[i]);
                Inventory[i] = null;
            }
        }
       
    }
}
