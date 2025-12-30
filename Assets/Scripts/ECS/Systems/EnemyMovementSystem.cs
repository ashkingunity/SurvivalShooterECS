using Ashking.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Ashking.Systems
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct EnemyMovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            
            foreach (var enemyTarget in SystemAPI.Query<RefRW<EnemyTarget>>().WithNone<InitializeEnemyTargetTag>().WithAll<EnemyTag>())
            {
                var navAgent = enemyTarget.ValueRO.Enemy.Value.navMeshAgent;
                navAgent.isStopped = false; 
                navAgent.destination =  playerPosition;
                
                // Update walking and idle animation
                enemyTarget.ValueRW.Enemy.Value.animator.SetBool("isWalking", navAgent.velocity.sqrMagnitude > 0.01f);
            }
        }
    }
}