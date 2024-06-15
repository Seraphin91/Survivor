using System;

namespace Survivor
{
    public class Poseidon : God
    {
        public Poseidon(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }
        
        public Poseidon()
        {
            throw new NotImplementedException("Delete This Constructor When PoseidonAdvanced Is Implemented");
        }

        public override void Miracle(Game game)
        {
            Cell[,] board = game.GetBoard() ;
            Patience = MaxPatience+1;
            int x;
            int y;
            do
            {
                x = ((GameIntermediate)game).Random.Next(0, board.GetLength(0));
                y = ((GameIntermediate)game).Random.Next(0, board.GetLength(1));
            } while (board[x,y] is not Forest);

            Cell nouv =new River(2, 3);
            if (board[x,y].GetContent() != null)
            {
                board[x,y].SetContent(null);
            }

            board[x, y] = nouv;
        }
        
        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            ((GameIntermediate)game).IncreaseDaysLeft(1);
        }
        public override string ToString()
        {
            return $"{GetType().Name} : {Patience}";
        }
    }
}
