using RunTime.Signal;
using UnityEngine;

namespace RunTime.Commands
{
    public class InputTakenCommand
    {
        private bool _isTouching;
        private bool _isFirstTimeTouchTaken;
        private Vector2? _mousePosition;
        public InputTakenCommand(bool isTouching, bool isFirstTimeTouchTaken, Vector2? mousePosition)
        {
            _mousePosition = mousePosition;
            _isTouching = isTouching;
            _isFirstTimeTouchTaken = isFirstTimeTouchTaken;
            
        }

        public void Execute()
        {
            
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
           

            
            Debug.LogWarning("Player is touching the screen");

        }
    }
}