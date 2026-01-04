using Unity.Entities;

namespace Ashking.Components
{
    public struct EnemyAttackData : IComponentData
    {
        public int Damage;
        public float CooldownTime;// timeBetweenAttacks
    }
}