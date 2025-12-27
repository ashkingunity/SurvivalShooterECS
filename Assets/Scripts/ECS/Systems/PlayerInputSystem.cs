using Ashking.Components;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Systems
{
    public partial class PlayerInputSystem : SystemBase
    {
        Game_IAS gameIAS;

        protected override void OnCreate()
        {
            gameIAS = new Game_IAS();
            gameIAS.Enable();
        }
        
        protected override void OnUpdate()
        {
            var moveInput = gameIAS.Player.Move.ReadValue<Vector2>();
            var lookInput =  gameIAS.Player.Look.ReadValue<Vector2>();
            foreach (var (moveDirection, lookDirection) in SystemAPI.Query<RefRW<MoveDirection>, RefRW<LookDirection>>().WithAll<PlayerTag>())
            {
                moveDirection.ValueRW.Value = moveInput;
                lookDirection.ValueRW.Value = lookInput;
            }
        }
    }
}