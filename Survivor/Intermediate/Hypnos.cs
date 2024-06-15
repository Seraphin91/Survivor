using System;

namespace Survivor
{
    public class Hypnos : God
    {
        public Hypnos(Item favourite, Item hated, int maxPatience):base(favourite,hated,maxPatience)
        {
        }

        public Hypnos()
        {
            throw new NotImplementedException("Delete This Constructor When HypnosAdvanced Is Implemented");
        }

        public override void Miracle(Game game)
        {
            Patience = MaxPatience+1; 
            Player joueur = ((GameIntermediate)game).GetPlayer();
            ((PlayerIntermediate)joueur).SetEnergy(5);

        }

        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            Player joueur = ((GameIntermediate)game).GetPlayer();
            ((PlayerIntermediate)joueur).SetEnergy(-3);
        }
        
        public override string ToString()
        {
            return $"{GetType().Name} : {Patience}";
        }
    }
}

