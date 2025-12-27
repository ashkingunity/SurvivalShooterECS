using Unity.Entities;
using Unity.Mathematics;

namespace Ashking.Components
{
    public struct MoveDirection : IComponentData
    {
        public float2 Value;
    }
}