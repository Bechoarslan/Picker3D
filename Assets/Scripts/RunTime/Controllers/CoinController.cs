using System;
using RunTime.Signal;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RunTime.Controllers
{
    public class CoinController : MonoBehaviour
    {
        private float _currentCoin = 0;
        [SerializeField] private float _winCoin;
        private float _multiplyiedValue;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            
           UISignals.Instance.onGetCoinValue += OnGetCoinValue;
        }

        private float OnGetCoinValue()
        {
            return _currentCoin;
        }


        
        private void OnLevelSuccessful()
        {
           
            _currentCoin +=CalculateCoin();
            
            
        }

        [Button("CalculateCoin")]
        private float CalculateCoin()
        {
           return (float)(_winCoin * CoreGameSignals.Instance.onGetMultiplyValue?.Invoke());
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            
            UISignals.Instance.onGetCoinValue -= OnGetCoinValue;
        }
    }
}