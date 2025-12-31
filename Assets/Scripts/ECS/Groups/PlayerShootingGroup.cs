using Unity.Entities;

namespace Ashking.Groups
{
    // Place the custom group (PlayerShootingGroup) within the default SimulationSystemGroup
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class PlayerShootingGroup : ComponentSystemGroup {}
}