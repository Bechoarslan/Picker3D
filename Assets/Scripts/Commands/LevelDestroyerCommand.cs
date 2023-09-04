using UnityEngine;

namespace Commands
{
    public class LevelDestroyerCommand
    {
        private Transform _levelHolder;

        public LevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            if (_levelHolder.transform.childCount <= 0) return;
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }

        
        
    }
}