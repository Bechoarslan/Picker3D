using System;
using System.Security.Cryptography;
using Keys;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Signal
{
    public class InputSignals : MonoBehaviour
    {
        #region Singleton

        public static InputSignals Instance;

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
        
        public UnityAction onFirstTimeTouchTaken = delegate {  };
        public UnityAction onInputTaken = delegate {  };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate {  };
        public UnityAction onInputReleased = delegate {  };
        public UnityAction onDisableInput = delegate {  };
        public UnityAction onEnableInput = delegate {  };
    }
}