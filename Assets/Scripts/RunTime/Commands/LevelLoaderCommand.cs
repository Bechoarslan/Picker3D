using UnityEngine;

namespace RunTime.Commands
{
    public class LevelLoaderCommand
    {
        private Transform _levelHolder;
        public LevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(byte levelIndex)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/Level/level {levelIndex}"),_levelHolder,true);
        }
    }
}