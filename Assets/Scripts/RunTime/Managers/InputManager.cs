using System.Collections.Generic;
using RunTime.Commands;
using RunTime.Data.UnityObjects;
using RunTime.Data.ValueObjects;
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
            Init();
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
                Debug.Log("AA");
                _inputReleasedCommand.Execute();
            }
            
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _inputTakenCommand.Execute();
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                _inputDraggedCommand.Execute();
                    
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