using System;
using Unity.Mathematics;

namespace RunTime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerForceData PlayerForceData;
        public MovementData MovementData;
        public PlayerMeshData PlayerMeshData;
    }

    [Serializable]
    public class PlayerMeshData
    {
        public float ScaleCounter;
    }

    [Serializable]
    public class MovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
        
    }

    [Serializable]
    public class PlayerForceData
    {
        public float3 ForceParameters;
    }
}