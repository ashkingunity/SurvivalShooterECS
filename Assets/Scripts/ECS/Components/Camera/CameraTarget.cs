using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct CameraTarget : IComponentData
    {
        public UnityObjectRef<Transform> CameraTargetTransform;
    }
}