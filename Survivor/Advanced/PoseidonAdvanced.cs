using System;

namespace Survivor
{
    public class PoseidonAdvanced : Poseidon
    {
        public PoseidonAdvanced(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }
        
        public override void Miracle(Game game)
        {
            Patience = MaxPatience + 1;
            int view = ((GameAdvanced)game).GetViewRange();
            ((GameAdvanced)game).SetViewRange(view+1);

        }
    }
}