using System;
using RunTime.Enums;
using RunTime.Signal;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelFail += OnLevelFail;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccesful;
        }

        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,1);
        }
        private void OnLevelInitialize(byte levelValue)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,0);
            UISignals.Instance.onSetNewLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
        }

        private void OnLevelSuccesful()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win,2);
        }

        private void OnLevelFail()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail,2);
        }

        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelFail -= OnLevelFail;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccesful;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
            
        }

       

        public void Play()
        {
            UISignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            InputSignals.Instance.onEnableInput?.Invoke();
            // CameraSignals.Instance.onSetCameraTarget?.Invoke();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void LevelFailed()
        {
            CoreGameSignals.Instance.onLevelFail?.Invoke();
        }
    }
}