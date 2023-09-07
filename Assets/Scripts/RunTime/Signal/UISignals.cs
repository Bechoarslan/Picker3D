using System;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signal
{
    public class UISignals : MonoBehaviour
    {
        #region Singleton

        public static UISignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion


        public UnityEvent<byte> onSetStageColor;
        public UnityEvent<byte> onSetNewLevelValue;
        public UnityEvent onPlay;

    }
}