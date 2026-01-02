using Unity.Entities;

namespace Ashking.Components
{
    public struct EnemyCooldownExpirationTimestamp : IComponentData, IEnableableComponent
    {
        public double Value;
    }
}