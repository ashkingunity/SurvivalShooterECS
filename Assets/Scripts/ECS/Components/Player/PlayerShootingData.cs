using Unity.Entities;

namespace Ashking.Components
{
    public struct PlayerShootingData : IComponentData
    {
        public int DamagePerShot; // The damage inflicted by each bullet
        public float TimeBetweenShots; // The time between each shot
        public float ShootingRange; // The distance the gun can fire
        public int ShootableMask; // A layer mask so the raycast only hits things on the shootable layer
    }
}