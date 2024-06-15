using System;

namespace Survivor
{
    public class GameAdvanced : GameIntermediate
    {
        protected int ViewRange;
        public GameAdvanced(Random random, int spawnRate, int daysLeft, int boardWidth, int boardHeight):base(random,spawnRate,daysLeft,boardWidth,boardHeight)
        {
            Player = new PlayerAdvanced(30, boardWidth / 2, boardHeight / 2);
            ViewRange = 5;
            Item plum = new Plum(2,9);
            Item coconut = new Coconut(4,5);
            Item banana = new Banana(3, 7);
            Item animal = new Animal(12);
            Gods = new God[]
            {
                new Demeter(plum,animal,4),
                new HypnosAdvanced(coconut,banana,2),
                new PoseidonAdvanced(banana,plum,2),
                new Tyche(animal,coconut,3),
                new Zeus(banana,animal,3)
            };
        }

        public GameAdvanced()
        {
            throw new NotImplementedException("Delete This Constructor When Game Hard Is Implemented");
        }

        public int GetViewRange()
        {
            return ViewRange;

        }
        public void SetViewRange(int viewRange)
        {
            ViewRange = viewRange;

        }
        
        protected override bool GetAction()
        {
            if (Player.GetEnergy() <= 0)
            {
                PrintEnd(false);
                return false;
            }
            else
            {
                char touch = (char) Console.Read();
                if (touch == 'w' || touch == 'a' || touch == 's' || touch == 'd')
                {
                    Player.Move(this,touch);
                    return true;
                }

                if (touch == 'n')
                {
                    if (Player.GetThirst() == false)
                    {
                        return NextDay();
                    }
                    else
                    {
                        char touch1 = ' ';
                        do
                        {
                            Console.WriteLine(
                                "If you are still thirsty, you will die during the night. Do you still want to end the day? (y/n)");
                            do
                            {
                                touch1 = (char)Console.Read();
                            } while (touch1 == 0x000d || touch1 == 0x000a);
                        } while (touch1 != 'y' && touch1 != 'n');    
                        if (touch1 == 'y')
                        {
                            return NextDay();
                        }

                        if (touch1 == 'n')
                        {
                            return true;
                        }
                    }
                }

                if (touch == 'q')
                {
                    return false;
                }

                if (touch == 'i')
                {
                    var (x, y) = Player.GetCoordinates();
                    if (Board[x,y] is River)
                    {
                        Player.Drink();
                    }

                    if (Board[x,y].GetContent() == null)
                    {
                        return true;
                    }
                    if (Board[x,y].GetContent() is Animal && Board[x,y].GetContent().GetIsEdible())
                    {
                        Player.Eat(Board[x,y].GetContent());
                        Board[x,y].SetContent(null);
                        return true;
                    }

                    if (Board[x,y].GetContent() is Animal)
                    {
                        ((Animal)Board[x,y].GetContent()).Kill();
                        return true;
                    }
                    else
                    {
                        if(Player.Eat(Board[x,y].GetContent()))
                        {
                            if (Board[x,y].GetContent() is not Tree)
                            {
                                Board[x,y].SetContent(null);
                            }
                            
                        }    
                        return true;
                    }
                }

                if (touch == 'z')
                {
                    var (x, y) = Player.GetCoordinates();
                    if (Board[x,y].GetContent() == null)
                    {
                        ((PlayerIntermediate)Player).Drop(Board);
                        return true;
                    }
                    else
                    {
                        ((PlayerIntermediate)Player).PickUp(Board);
                        return true;
                    }
                }

                if (touch == 'p')
                {
                    var (x, y) = Player.GetCoordinates();
                    ((PlayerAdvanced)Player).Plant(Board[x,y]);
                }

                return true;
            }

        }
        
        protected override void UpdateBoard()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i,j].GetContent() != null)
                    {
                        Board[i,j].Update();
                    }

                    if (Board[i,j].GetContent() is Tree && ((Tree)Board[i,j].GetContent()).GetGrowth() == 0)
                    {
                        SpawnItem(i,j);
                    }
                }
            }
        }
        
        private bool IsInRange(int x, int y)
        {
            var (i, j) = Player.GetCoordinates();
            double distance = Math.Sqrt((x-i)*(x-i)+(y*2-j*2)*(y*2-j*2));
            if (distance <= ViewRange)
            {
                return true;
            }

            return false;
        }

        protected override void PrintBoard()
        {
            ConsoleColor bg = Console.BackgroundColor;
            ConsoleColor fg = Console.ForegroundColor;
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                Console.Write("|");
                for (int x = 0; x < Board.GetLength(0); x++)
                {

                    if (!IsInRange(x, y))
                    {
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = fg;
                        Console.Write(" ");
                    }
                    else
                    {
                        switch (Board[x, y])
                    {
                        case Forest:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            break;
                        case River:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case Sea:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            break;
                        default:
                            Console.BackgroundColor = bg;
                            break;
                    }

                    if (Player.GetCoordinates().x == x && Player.GetCoordinates().y == y)
                        Console.Write("X");
                    else
                        Console.Write(Board[x, y]);
                    }
                }
                Console.BackgroundColor = bg;
                Console.ForegroundColor = fg;
                Console.WriteLine("|");
            }
        }
    }
}