using Ashking.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial struct EnemyAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            foreach (var (expirationTimestamp, cooldownEnabled) in SystemAPI.Query<EnemyCooldownExpirationTimestamp, EnabledRefRW<EnemyCooldownExpirationTimestamp>>())
            {
                if (elapsedTime < expirationTimestamp.Value) 
                    continue;
                
                cooldownEnabled.ValueRW = false;// enemy can attack when cooldown is disabled
            }

            var attackJob = new EnemyAttackJob
            {
                PlayerLookup = SystemAPI.GetComponentLookup<PlayerTag>(true),
                AttackDataLookup = SystemAPI.GetComponentLookup<EnemyAttackData>(true),
                CooldownLookup = SystemAPI.GetComponentLookup<EnemyCooldownExpirationTimestamp>(),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageThisFrame>(),
                ElapsedTime = elapsedTime
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = attackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
    
    [BurstCompile]
    public struct EnemyAttackJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerLookup;
        [ReadOnly] public ComponentLookup<EnemyAttackData> AttackDataLookup;
        public ComponentLookup<EnemyCooldownExpirationTimestamp> CooldownLookup;
        public BufferLookup<DamageThisFrame> DamageBufferLookup;

        public double ElapsedTime;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity playerEntity;
            Entity enemyEntity;

            if (PlayerLookup.HasComponent(triggerEvent.EntityA) && AttackDataLookup.HasComponent(triggerEvent.EntityB))
            {
                playerEntity = triggerEvent.EntityA;
                enemyEntity = triggerEvent.EntityB;
            }
            else if (PlayerLookup.HasComponent(triggerEvent.EntityB) && AttackDataLookup.HasComponent(triggerEvent.EntityA))
            {
                playerEntity = triggerEvent.EntityB;
                enemyEntity = triggerEvent.EntityA;
            }
            else
            {
                return;
            }
            
            // enemy cannot attack when cooldown is enabled
            if (CooldownLookup.IsComponentEnabled(enemyEntity)) 
                return;

            var attackData = AttackDataLookup[enemyEntity];
            CooldownLookup[enemyEntity] = new EnemyCooldownExpirationTimestamp { Value = ElapsedTime + attackData.CooldownTime };
            CooldownLookup.SetComponentEnabled(enemyEntity, true);// enemy cannot attack when cooldown is enabled

            var playerDamageBuffer = DamageBufferLookup[playerEntity];
            playerDamageBuffer.Add(new DamageThisFrame
            {
                Value = attackData.Damage
            });
        }
    }
}