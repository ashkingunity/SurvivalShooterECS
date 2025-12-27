using Unity.Entities;
using Unity.Mathematics;

namespace Ashking.Components
{
    public struct LookDirection : IComponentData
    {
        public float2 Value;
    }
}