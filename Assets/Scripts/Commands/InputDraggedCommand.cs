using Keys;
using Signal;
using Unity.Mathematics;
using UnityEngine;

namespace Commands
{
    public class InputDraggedCommand
    {
        private bool _isTouching;
        private Vector2? _mousePosition;
        private float3 _moveVector;
        private float _currentVelocity;
        private float _dataClampSpeed;
        private float _horizontalInputSpeed;
        
       

        public InputDraggedCommand(bool isTouching, Vector2? mousePosition, float3 moveVector, float currentVelocity, float dataClampSpeed, float dataHorizontalInputSpeed)
        {
            _isTouching = isTouching;
            _mousePosition = mousePosition;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
            _dataClampSpeed = dataClampSpeed;
            _horizontalInputSpeed = dataHorizontalInputSpeed;
        }

        public void Execute()
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                    if (mouseDeltaPos.x > _horizontalInputSpeed)
                    {
                        _moveVector.x = _horizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else if (mouseDeltaPos.x > _horizontalInputSpeed)
                    {
                        _moveVector.x = -_horizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    }
                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x,0f,ref _currentVelocity,_dataClampSpeed);
                    }

                    _moveVector.x = mouseDeltaPos.x;
                    _mousePosition = Input.mousePosition;
                    
                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizontalValue = _moveVector.x,
                        ClampValue = _dataClampSpeed
                    });
                    Debug.LogWarning("Player Dragged");
                }
            }
        }
    }
}