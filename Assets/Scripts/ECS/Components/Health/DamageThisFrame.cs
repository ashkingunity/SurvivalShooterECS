using Unity.Entities;

namespace Ashking.Components
{
    // used to accumulate points of damage done during a frame, and apply them to the player's current health in a Centralized Health System.
    public struct DamageThisFrame : IBufferElementData
    {
        public float Value;
    }
}