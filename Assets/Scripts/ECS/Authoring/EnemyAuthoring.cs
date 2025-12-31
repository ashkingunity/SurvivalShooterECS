using Ashking.Components;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
            
        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent<InitializeCharacterFlag>(entity);
                
                AddComponent<InitializeEnemyTargetTag>(entity);
                AddComponent<EnemyTarget>(entity);
                
                AddComponent(entity, new CurrentHealth
                {
                    Value = authoring.maxHealth
                });
            }
        }
    }
}