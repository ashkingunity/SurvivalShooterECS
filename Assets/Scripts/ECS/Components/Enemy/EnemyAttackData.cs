using Unity.Entities;

namespace Ashking.Components
{
    public struct EnemyAttackData : IComponentData
    {
        public float Damage;
        public float CooldownTime;// timeBetweenAttacks
    }
}