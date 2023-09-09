using System;
using RunTime.Data.ValueObjects;
using RunTime.Keys;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;



        #endregion

        #region Private Variables

        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _xValue;
        private float2 _clampValues;
        private MovementData _data;



        #endregion

        #endregion


        internal void SetData(MovementData data)
        {
            _data = data;

        }
        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (_isReadyToMove)
            {
                MovePlayer();
            }

            else
            {
                StopHorizontally();
            }
        }

        

        private void StopHorizontally()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void StopPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void MovePlayer()
        {
            
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_xValue * _data.SidewaysSpeed, velocity.y, _data.ForwardSpeed);
            rigidbody.velocity = velocity;
            var position1 = rigidbody.position;
            Vector3 position;
            position = new Vector3(Mathf.Clamp(position1.x, _clampValues.x, _clampValues.y), (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
            

        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }

        internal void UpdateInputParams(HorizontalInputParams inputParams)
        {
            _xValue = inputParams.HorizontalValue;
            _clampValues = inputParams.ClampValue;
        }

        internal void OnReset()
        {
            StopPlayer();;
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }
    }
}