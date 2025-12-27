using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct PlayerAnimatorTarget : IComponentData
    {
        public UnityObjectRef<Animator> Animator;
    }
}