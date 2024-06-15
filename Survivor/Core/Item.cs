using System;

namespace Survivor
{
    public abstract class Item : IUpdate
    { 
        protected int Expiry;
        protected int EnergyAmount;
        protected bool IsEdible;
        
        protected Item(int expiry, int energyAmount, bool isEdible)
        {
            Expiry = expiry;
            EnergyAmount = energyAmount;
            IsEdible = isEdible;
        }

        public abstract void Update();
        
        public int GetEnergyAmount()
        {
            return EnergyAmount;
        }
        
        public int GetExpiry()
        {
            return Expiry;
        }
        
        public bool GetIsEdible()
        {
            return IsEdible;
        }
    }
}