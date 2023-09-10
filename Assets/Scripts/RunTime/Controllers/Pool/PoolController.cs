using System;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObjects;
using RunTime.Data.ValueObjects;
using RunTime.Signal;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RunTime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<DOTweenAnimation> tween = new List<DOTweenAnimation>();
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stadeID;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float3 poolAfterColor = new float3(0.1607843f, 0.3144797f, 0.6039216f);

        #endregion

        #region Private Values

        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _collectedCount;
        [ShowInInspector] private LevelData _levelData;
        private readonly string _collectable = "Collectable";
       

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
            _levelData = GetLevelData();


        }
        
        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].PoolList[stadeID];
        }

        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()];

        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActiveTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onGetCollectedObjectValue += OnGetCollectedObjectValue;
            
        }

        private short OnGetCollectedObjectValue()
        {
            var result = 0;
            foreach (var collectables in _levelData.CollectedObjectCount)
            {
                result += (collectables * 100) / _levelData.TotalCollectableCount;
                
            }
            

            return (short)result;
        }

        private void OnStageAreaSuccessful(byte stageValue)
        {
            if(stageValue != stadeID) return;
            _levelData.CollectedObjectCount.Add(_collectedCount);
        }
        

        private void OnActiveTweens(byte stageValue)
        {
            
            if (stageValue != stadeID) return;
            foreach (var tween in tween)
            {
                tween.DOPlay();
                

            }
        }
        private void OnChangePoolColor(byte stageValue)
        {
            if (stageValue != stadeID) return;
            renderer.material.DOColor(new Color(poolAfterColor.x, poolAfterColor.y, poolAfterColor.z, 1), .5f)
                .SetEase(Ease.Flash).SetRelative(false);
        }
        
        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }
        
        public bool TakeResults(byte managerStateValue)
        {
            if (stadeID == managerStateValue)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_collectable)) return;
            _collectedCount++;
            SetCollectedAmountToPool();
        }
        
        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
           
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_collectable)) return;
            _collectedCount--;
            SetCollectedAmountToPool();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnActiveTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangePoolColor;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetCollectedObjectValue -= OnGetCollectedObjectValue;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnReset()
        {
            _levelData.CollectedObjectCount.Clear();
            
        }


       
      
    }
}