using System.Collections.Generic;
using RunTime.Commands;
using RunTime.Data.UnityObjects;
using RunTime.Data.ValueObjects;
using RunTime.Keys;
using RunTime.Signal;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RunTime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private InputData _data;
        private bool _isAvailableForTouch,_isFirstTimeTouchTaken,_isTouching;
        
        private InputDraggedCommand _inputDraggedCommand;
        private InputTakenCommand _inputTakenCommand;
        private InputReleasedCommand _inputReleasedCommand;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;


        #endregion

        #endregion


        private void Awake()
        {
            _data = GetInputData();
            
        }

        private void Init()
        {
            _inputReleasedCommand = new InputReleasedCommand();
            _inputTakenCommand = new InputTakenCommand(_isTouching,_isFirstTimeTouchTaken,_mousePosition);
            _inputDraggedCommand = new InputDraggedCommand(_isTouching,_mousePosition,_moveVector,_currentVelocity,_data.ClampSpeed,_data.HorizontalInputSpeed);



        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }
        

        private void OnReset()
        {
            _isTouching = false;
            _isAvailableForTouch = false;
            //isFirstTimeTouchTaken = false;
        }
        
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                        if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                            _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                            _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                _data.ClampSpeed);

                        _moveVector.x = mouseDeltaPos.x;

                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            HorizontalValue = _moveVector.x,
                            ClampValue = _data.ClampValue
                        });
                    }
                }
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData,results);
            return results.Count > 0;
        }
    }
}