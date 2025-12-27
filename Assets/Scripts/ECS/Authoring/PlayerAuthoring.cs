using Ashking.Components;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 2;
        [SerializeField] float lookSpeed = 2;
        
        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerMoveSpeed
                {
                    Value = authoring.moveSpeed
                });
                AddComponent(entity, new PlayerRotationSpeed
                {
                    Value = authoring.lookSpeed
                });
                AddComponent<MoveDirection>(entity);
                AddComponent<LookDirection>(entity);
            }
        }
    }
}