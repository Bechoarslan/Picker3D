using System.Collections.Generic;
using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Multi", menuName = "3DPicker/CD_Multi", order = 0)]
    public class CD_Multi : ScriptableObject
    {
        public MultiplyValues _data;

    }
}