using System;
using System.Collections.Generic;
using RunTime.Data.UnityObjects;
using RunTime.Enums;
using RunTime.Signal;
using Unity.VisualScripting;
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

        private void Awake()
        {
           SetDictionary();
        }

        private void OnEnable()
        {
            UISignals.Instance.onPlay += GetDictionary;
            
        }

        private void GetDictionary()
        {
            CoreGameSignals.Instance.onSetMultiplier?.Invoke(_multiplier);
        }

        private void OnDisable()
        {
            
            UISignals.Instance.onPlay += GetDictionary;
        }


        private void SetDictionary()
        {
            for (int i = 0; i < multiplyTransform.Count; i++)
            {
                Transform newTransform = multiplyTransform[i];
                float newFloat = Resources.Load<CD_Multi>("Data/CD_Multi")._data.multiplyValues[i];
                
                _multiplier.Add(newTransform, newFloat);

            }
        }
        
       

    }
        }
    
