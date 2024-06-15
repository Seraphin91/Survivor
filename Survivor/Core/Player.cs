using System;

namespace Survivor
{
    public class Player
    {
        protected readonly int MaxEnergy;
        protected int Energy;
        protected bool Thirst;
        protected double Luck;
        protected (int x, int y) Coordinates;

        public Player(int maxEnergy, int x, int y)
        {
            MaxEnergy = maxEnergy;
            Energy = maxEnergy;
            Thirst = true;
            Luck = 1.0;
            Coordinates = (x, y);
        }
        public Player()
        {
            throw new NotImplementedException("Delete This Constructor When Player Hard Is Implemented");
        }

        public double GetLuck()
        {
            return Luck;
        }

        public void SetLuck(double luck)
        {
            Luck = luck;
        }

        public (int x, int y) GetCoordinates()
        {
            return Coordinates;
        }

        public int GetEnergy()
        {
            return Energy;
        }

        public bool GetThirst()
        {
            return Thirst;
        }

        public virtual void Move(Game game, char key)
        {
            Cell[,] board = game.GetBoard();
            if (board[Coordinates.x,Coordinates.y] is not Sea)
            { 
                switch(key)
                {
                    case 'w':
                        if (board[Coordinates.x,Coordinates.y-1] is Sea)
                        {
                            break;
                        }
                        Coordinates.y -= 1;
                        Energy -= board[Coordinates.x, Coordinates.y].GetMoveCost();
                        break;
                    case 'a':
                        if (board[Coordinates.x-1,Coordinates.y] is Sea)
                        {
                            break;
                        }
                        Coordinates.x -= 1;
                        Energy -= board[Coordinates.x, Coordinates.y].GetMoveCost();
                        break;
                    case 's':
                        if (board[Coordinates.x,Coordinates.y+1] is Sea)
                        {
                            break;
                        }
                        Coordinates.y += 1;
                        Energy -= board[Coordinates.x, Coordinates.y].GetMoveCost();
                        break;
                
                    case 'd':
                        if (board[Coordinates.x+1,Coordinates.y] is Sea)
                        {
                            break;
                        }
                        Coordinates.x += 1;
                        Energy -= board[Coordinates.x, Coordinates.y].GetMoveCost();
                        break;
                }
            }
            
        }

        public bool SpendTheNight()
        {
            Console.WriteLine("You spend the night on the ground.");
            Energy -= 2;
            if (Energy <= 0 || Thirst)
            {
                return false; 
            }

            Thirst = true;

            return true;
        }
        
        public bool Eat(Item food)
        {
            if (food == null)
            {
                throw new ArgumentNullException("There is no food to eat.");
            }
            if (Energy+ food.GetEnergyAmount() >MaxEnergy)
            {
                return false;
            }
            else
            {
                Energy += food.GetEnergyAmount();
                return true;
            }
        }
        
        public void Drink()
        {
            Thirst = false;
        }

        public override string ToString()
        {
            return "^";
        }
    }
}



