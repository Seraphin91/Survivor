using System;

namespace Survivor
{
    public class HypnosAdvanced : Hypnos
    {
        public HypnosAdvanced(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }
        
        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            Cell[,] board = game.GetBoard();
            for (int i = 0; i < board.GetLength(0) ; i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int cost = board[i,j].GetMoveCost();
                    board[i,j].SetMoveCost(cost+1);
                }
            }

        }
    }
}