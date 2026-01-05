using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ashking.Components
{
    public struct EnemySpawnData : IComponentData
    {
        public Entity EnemyPrefab;
        public float SpawnInterval;
        public float3 SpawnPoint;
    }
}