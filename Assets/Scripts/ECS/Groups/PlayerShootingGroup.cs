using Unity.Entities;

namespace AshKing.Groups
{
    // Place the custom group (PlayerShootingGroup) within the default SimulationSystemGroup
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class PlayerShootingGroup : ComponentSystemGroup {}
}