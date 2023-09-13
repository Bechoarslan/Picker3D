
using System;
using System.Collections.Generic;

using RunTime.Data.UnityObjects;

using RunTime.Signal;

using UnityEngine;

namespace RunTime.Controllers
{
   
    public class MultiplyController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> multiplyTransform;
        private readonly Dictionary<Transform, float> _multiplier = new Dictionary<Transform, float>();
        #endregion
        #endregion

       
        private void OnEnable()
        {
            UISignals.Instance.onPlay += GetDictionary;
            CoreGameSignals.Instance.onReset += OnReset;


        }

      

        private void GetDictionary()
        {
            SetDictionary();
            CoreGameSignals.Instance.onSetMultiplier?.Invoke(_multiplier);
           
            
        }
        private void OnDisable()
        {
            
            UISignals.Instance.onPlay -= GetDictionary;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        
        private void SetDictionary()
        {
            for (var i = 0; i < multiplyTransform.Count; i++)
            {
                Transform newTransform = multiplyTransform[i];
                float newFloat = Resources.Load<CD_Multi>("Data/CD_Multi")._data.multiplyValues[i];
                
                _multiplier.Add(newTransform, newFloat);

            }
            
        }
        
        private void OnReset()
        {
            _multiplier.Clear();
        }

        
    }
        }
    
