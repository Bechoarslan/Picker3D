using Signal;
using UnityEngine;

namespace Commands
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
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
           

            _mousePosition = Input.mousePosition;
            Debug.LogWarning("Player is touching the screen");

        }
    }
}