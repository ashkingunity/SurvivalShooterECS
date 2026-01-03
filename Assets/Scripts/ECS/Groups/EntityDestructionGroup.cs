using Unity.Entities;

namespace Ashking.Groups
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial class EntityDestructionGroup : ComponentSystemGroup {}
}