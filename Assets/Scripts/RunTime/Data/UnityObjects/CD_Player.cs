using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "3DPicker/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;

    }
}