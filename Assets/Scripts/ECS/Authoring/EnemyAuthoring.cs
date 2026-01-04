using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField] EnemyName enemyName;
        
        [SerializeField] float maxHealth = 100f;
        [SerializeField] int attackDamage = 5;
        [SerializeField] float coolDownTime = 0.5f;
        
        [Space]
        public int scoreValue = 10;
            
        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent<InitializeCharacterFlag>(entity);
                
                AddComponent<InitializeEnemyGameObjectDataTag>(entity);
                AddComponent(entity, new EnemyGameObjectData
                {
                    EnemyName = authoring.enemyName
                });

                AddComponent(entity, new CurrentHealth
                {
                    Value = authoring.maxHealth
                });
                
                AddComponent(entity, new EnemyAttackData
                {
                    Damage = authoring.attackDamage,
                    CooldownTime = authoring.coolDownTime
                });
                AddComponent<EnemyCooldownExpirationTimestamp>(entity);
                SetComponentEnabled<EnemyCooldownExpirationTimestamp>(entity, false);
                
                AddComponent<DestroyEntityFlag>(entity);
                SetComponentEnabled<DestroyEntityFlag>(entity, false);
                
                AddComponent(entity, new Score
                {
                    Value = authoring.scoreValue
                });
            }
        }
    }
}