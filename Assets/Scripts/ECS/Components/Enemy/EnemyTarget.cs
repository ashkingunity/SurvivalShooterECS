using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Components
{
    public struct EnemyTarget : IComponentData
    {
        public UnityObjectRef<Enemy> Enemy;
    }
}