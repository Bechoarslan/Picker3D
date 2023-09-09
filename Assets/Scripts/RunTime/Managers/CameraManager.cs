using System;
using Cinemachine;
using RunTime.Signal;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera cinemachine;
        
        #endregion

        #region Private Variables

        private float3 cameraPosition;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            cameraPosition = transform.position;
        }


        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        }

        private void OnSetCameraTarget()
        {
            var player = FindObjectOfType<PlayerManager>().transform;
            cinemachine.Follow = player;
            //cinemachine.LookAt = player;

        }

        private void OnReset()
        {
            transform.position = cameraPosition;
        }
        
        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }
    }
}