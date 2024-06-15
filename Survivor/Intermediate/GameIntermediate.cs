using System;

namespace Survivor
{
    public class GameIntermediate: Game
    {
        protected God[] Gods;

        public GameIntermediate(Random random, int spawnRate, int daysLeft, int boardWidth, int boardHeight):base(random,spawnRate,daysLeft,boardWidth,boardHeight)
        {
            Player = new PlayerIntermediate(30, boardWidth / 2, boardHeight / 2);
            Item plum = new Plum(2,9);
            Item coconut = new Coconut(4,5);
            Item banana = new Banana(3, 7);
            Item animal = new Animal(12);
            Gods = new God[]
            {
                new Demeter(plum,animal,4),
                new Hypnos(coconut,banana,2),
                new Poseidon(banana,plum,2),
                new Tyche(animal,coconut,3)
            };
        }

        public GameIntermediate()
        {
            throw new NotImplementedException("Delete This Constructor When Game Hard Is Implemented");
        }

        public void SetSpawnRate(int newSpawnRate)
        {
            SpawnRate = newSpawnRate;
        }
        
        public int GetSpawnRate()
        {
            return SpawnRate;
        }
        
        public Player GetPlayer()
        {
            return Player;
        }

        public void IncreaseDaysLeft(int days)
        {
            DaysLeft += days;
        }
        
        protected void MakeOffering()
        {
            char touch;
            do
            {
                Console.WriteLine("If you want to make an offering, select the god to whom you want to sacrifice. (quit with 'q').");
                do
                {
                    touch = (char)Console.Read();
                } while (touch == 0x000d || touch == 0x000a);
            } while ((touch < 48 || touch > (Gods.Length+1)+48) && touch != 'q');

            if (touch != 'q')
            {
                ((PlayerIntermediate)Player).Sacrifice(this,Gods[touch-'0']);
            }
                
        }
        
        protected override void SpawnItem(int x, int y)
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
                
            } while (x + x2 < 0 || x + x2 > Board.GetLength(0)-1 || y + y2 < 0 || y + y2 > Board.GetLength(1)-1 );
            Cell nouvcell = Board[x + x2, y + y2];
            if (nouvcell.GetContent() == null)
            {
                switch (debutcell)
                {
                    case Forest :
                        int all = Random.Next(0, 10);
                        if (all <= 1)
                        {
                            Animal d = new Animal(12);
                            nouvcell.SetContent(d);
                        }

                        if (all >= 2)
                        {
                            Banana a =new Banana(3, 7);
                            nouvcell.SetContent(a);
                        }
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
        
        protected override bool NextDay()
        {
            DaysLeft -= 1;
            if (Player.GetThirst())
            {
                PrintEnd(false);
                return false;
            }
            MakeOffering();
            for (int i = 0; i < Gods.Length; i++)
            {
                if (Gods[i].GetPatience() == 1)
                {
                    Gods[i].Disaster(this);
                }
                Gods[i].Update();
            }
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
            
            UpdateBoard();
            Spawn();
            return true;
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
                            Board[x,y].SetContent(null);
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

                return true;
            }
        }    
        
        protected void PrintGods()
        {
            Console.Write("|");
            for (int i = 0; i < Gods.Length; i++)
            {
                Console.Write(" ");
                Console.Write(Gods[i].ToString());
                Console.Write(" |");
            }
            Console.WriteLine();
        }

        protected void PrintInventory()
        {
            Console.Write("|");
            Item[] a = ((PlayerIntermediate)Player).GetInventory();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == null)
                {
                    Console.Write(" ");
                    Console.Write(" |");
                }
                else
                {
                    Console.Write(" ");
                    Console.Write(a[i].ToString());
                    Console.Write(" |");
                }
            }
            Console.WriteLine();
        }

        protected override void PrintAll()
        {
            PrintGods();
            PrintBoard();
            PrintStats();
            PrintInventory();
        }
    }
}
