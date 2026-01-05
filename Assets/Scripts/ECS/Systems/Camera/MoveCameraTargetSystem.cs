using Ashking.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Ashking.Systems
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct MoveCameraSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, cameraTarget) in 
                     SystemAPI.Query<LocalToWorld, CameraTarget>().WithNone<InitializeCameraTargetTag>().WithAll<PlayerTag>())
            {
                cameraTarget.CameraTargetTransform.Value.position = transform.Position;
            }
        }
    }
}