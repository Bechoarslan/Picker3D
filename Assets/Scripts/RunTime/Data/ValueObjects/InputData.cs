using System;
using Unity.Mathematics;

namespace RunTime.Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        
        public float HorizontalInputSpeed;
        public float2 ClampValue;
        public float ClampSpeed;
    }
}