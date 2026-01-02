using Ashking.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

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
            
            foreach (var (enemyGameObjectData, localTransform) in SystemAPI.Query<RefRW<EnemyGameObjectData>, RefRW<LocalTransform>>().WithNone<InitializeEnemyGameObjectDataTag>().WithAll<EnemyTag>())
            {
                // Move unity's navmesh agent
                enemyGameObjectData.ValueRO.NavMeshAgent.Value.isStopped = false; 
                enemyGameObjectData.ValueRO.NavMeshAgent.Value.destination =  playerPosition;
                
                // Move enemy entity
                localTransform.ValueRW.Position = enemyGameObjectData.ValueRO.NavMeshAgent.Value.transform.position;
                
                // Update walking and idle animation
                enemyGameObjectData.ValueRW.Animator.Value.SetBool("isWalking", enemyGameObjectData.ValueRO.NavMeshAgent.Value.velocity.sqrMagnitude > 0.01f);
            }
        }
    }
}