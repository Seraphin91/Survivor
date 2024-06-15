using System;

namespace Survivor
{
    public class Zeus : God
    {
        public Zeus(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }

        public override void Miracle(Game game)
        {
            Patience = MaxPatience+1;
            Cell[,] board = game.GetBoard();
            for (int i = 0; i < board.GetLength(0) ; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j].GetContent() is Tree)
                    {
                        Item arbre = board[i, j].GetContent();
                        if (((Tree)arbre).GetGrowth() != 0)
                        {
                            int age = ((Tree)arbre).GetGrowth();
                            ((Tree)arbre).SetGrowth(age-1);
                        }
                    }
                }
            }

        }
        
        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            Cell[,] board = game.GetBoard();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j].GetContent() is Tree)
                    {
                        board[i,j].SetContent(null);
                    }
                }
            }

        }

        public override string ToString()
        {
            return $"{GetType().Name} : {Patience}";
        }
    }
}
