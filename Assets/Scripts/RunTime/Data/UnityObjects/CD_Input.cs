using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "3DPicker/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;

    }
}