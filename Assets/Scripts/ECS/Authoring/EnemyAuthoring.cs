using Ashking.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace Ashking.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent<InitializeCharacterFlag>(entity);
                
                AddComponent<InitializeEnemyTargetTag>(entity);
                AddComponent<EnemyTarget>(entity);
            }
        }
    }
}