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


        public UnityAction<byte> onSetStageColor = delegate {  };
        public UnityAction<byte> onSetNewLevelValue = delegate { };
        public UnityAction onPlay = delegate {  };
        public UnityAction<float> onSetPercantageValue = delegate {  };
        public UnityAction<float> onSetCoinText = delegate {  };
        public Func<float> onGetCoinValue = delegate { return 0; };
      


    }
}