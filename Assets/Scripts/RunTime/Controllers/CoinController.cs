using System;
using RunTime.Signal;
using UnityEngine;

namespace RunTime.Controllers
{
    public class CoinController : MonoBehaviour
    {
        private void OnEnable()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccesfull;
        }

        private void OnLevelSuccesfull()
        {
           Debug.Log(CoreGameSignals.Instance.onGetMultiplyValue?.Invoke()); 
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccesfull;
        }
    }
}