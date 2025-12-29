using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct AnimatorTarget : IComponentData
    {
        public UnityObjectRef<Animator> Animator;
    }
}