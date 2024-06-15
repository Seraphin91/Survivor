using System;

namespace Survivor
{
    public class Tyche : God
    {
        public Tyche(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }

        public override void Miracle(Game game)
        {
            Patience = MaxPatience+1;
            Player joueur = ((GameIntermediate)game).GetPlayer();
            double luck = ((PlayerIntermediate)joueur).GetLuck();
            ((PlayerIntermediate)joueur).SetLuck(luck+0.25);
        }
        
        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            Player joueur = ((GameIntermediate)game).GetPlayer();
            double luck = ((PlayerIntermediate)joueur).GetLuck();
            ((PlayerIntermediate)joueur).SetLuck(luck-0.25);
        }
        
        public override string ToString()
        {
            return $"{GetType().Name} : {Patience}";
        }
    }
}

