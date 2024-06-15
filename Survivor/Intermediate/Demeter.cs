using System;

namespace Survivor
{
    public class Demeter : God
    {
        public Demeter(Item favourite, Item hated, int maxPatience) :base(favourite,hated,maxPatience)
        {
        }
        
        public override void Miracle(Game game)
        {
            Patience = MaxPatience+1;
            int rate = ((GameIntermediate)game).GetSpawnRate();
            ((GameIntermediate)game).SetSpawnRate(rate+5);

        }

        public override void Disaster(Game game)
        {
            Patience = MaxPatience+1;
            int rate = ((GameIntermediate)game).GetSpawnRate();
            ((GameIntermediate)game).SetSpawnRate(rate-3);
        }
        
        public override string ToString()
        {
            return $"{GetType().Name} : {Patience}";
        }
    }
}

