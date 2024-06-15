using System;

namespace Survivor
{
    public class Game
    {
        public Random Random;
        protected int SpawnRate;
        protected int DaysLeft;
        protected Cell[,] Board;
        protected Player Player;
        public Game(Random random, int spawnRate, int daysLeft, int boardWidth, int boardHeight)
        {
            Random = random;
            SpawnRate = spawnRate;
            DaysLeft = daysLeft;
            if (boardWidth<5 || boardHeight < 5)
            {
                throw new ArgumentException("Board must be at least 5x5");
            }
            Board = CreateBoard(boardWidth, boardHeight);
            Player = new Player(30, boardWidth/2,boardHeight/2);
        }
        
        public Game()
        {
            throw new NotImplementedException("Delete This Constructor When Game Hard Is Implemented");
        }
        
        public Cell[,] GetBoard()
        {
            return Board;
        }

        protected virtual void SpawnItem(int x, int y)
        {
            Cell debutcell = Board[x, y];
            int x2;
            int y2;
            do
            {
                int range = Random.Next(1, debutcell.GetSpawnRange()+1);
                x2 = Random.Next(0, range);
                y2 = x2 - range;
                int chance = Random.Next(0, 2);
                if (chance == 0)
                {
                    x2 *= -1;
                }
                int chance1 = Random.Next(0, 2);
                if (chance1 == 0)
                {
                    y2 *= -1;
                }
                
            } while (x + x2 < 0 || x + x2 > Board.GetLength(0)-1|| y + y2 < 0 || y + y2 > Board.GetLength(1)-1);
            Cell nouvcell = Board[x + x2, y + y2];
            if (nouvcell.GetContent() == null)
            {
                switch (debutcell)
                {
                    case Forest :
                        Banana a =new Banana(3, 7);
                        nouvcell.SetContent(a);
                        break;
                    case Sea :
                        Coconut b =new Coconut(4, 5);
                        nouvcell.SetContent(b);
                        break;
                    case River :
                        Plum c =new Plum(2, 9);
                        nouvcell.SetContent(c);
                        break;
                }
            }

        }

        protected void Spawn()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    int all = Random.Next(1, 100);
                    if (all <= SpawnRate*Player.GetLuck())
                    {
                        SpawnItem(i,j);
                    }
                }
            }
        }

        protected virtual void PrintBoard()
        {
            ConsoleColor bg = Console.BackgroundColor;
            ConsoleColor fg = Console.ForegroundColor;
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                Console.Write("|");
                for (int x = 0; x < Board.GetLength(0); x++)
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
                Console.BackgroundColor = bg;
                Console.ForegroundColor = fg;
                Console.WriteLine("|");
            }
        }
        
        protected void PrintStats()
        {
            Console.WriteLine("Days left: {0}", DaysLeft);
            Console.WriteLine("Energy: {0}",Player.GetEnergy());
            Console.WriteLine("Thirst: {0}",Player.GetThirst());
        }
        
        protected virtual void PrintAll()
        {
            PrintBoard();
            PrintStats();
        }

        protected void PrintEnd(bool win)
        {
            if (win)
            {
                Console.WriteLine("You survived the journey!");
            }
            else
            {
                Console.WriteLine("You died.");
            }
        }
        protected virtual void UpdateBoard()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i,j].GetContent() != null)
                    {
                        Board[i,j].Update();
                    }
                }
            }
        }
        
        protected virtual bool NextDay()
        {
            bool a = Player.SpendTheNight();
            if (a == false)
            {
                PrintEnd(false);
                return false;
            }
            if (DaysLeft == 0)
            {
                PrintEnd(true);
                return false;
            }

            DaysLeft -= 1;
            UpdateBoard();
            Spawn();
            return true;
        }
        
        protected virtual bool GetAction()
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
                    else
                    {
                        if(Player.Eat(Board[x,y].GetContent()))
                        {
                            Board[x,y].SetContent(null);
                        }    
                        return true;
                    }
                }

                return true;
            }
        }

        public void Play()
        {
            Spawn();
            do
            {
                PrintAll();
            } while (GetAction());
        }

        

        protected virtual Cell[,] CreateBoard(int width, int height)
        {
            Cell[,] board = new Cell[width, height];
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (x == 0 || x == board.GetLength(0) - 1 || y == 0 || y == board.GetLength(1) - 1)
                        board[x, y] = new Sea(1, 2);
                    else if (x == 2 * y)
                        board[x, y] = new River(2, 2); 
                    else
                        board[x, y] = new Forest(1, 2);
                }
            }

            return board;
        }
    }
}
