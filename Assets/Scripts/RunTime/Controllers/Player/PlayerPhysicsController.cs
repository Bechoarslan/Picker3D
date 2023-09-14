
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Controllers.Pool;

using RunTime.Managers;
using RunTime.Signal;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private Collider _collider;
        Dictionary<Transform, float> dic = new Dictionary<Transform, float>();
        
        
        #endregion

        #region Private Variables
        private readonly string _stageArea = "StageArea";
        private readonly string _finish = "FinishArea";
        private readonly string _miniGame = "MiniGameArea";
        private float _multiplyValue = 0; 
        private bool isEnded = false;
     

        #endregion

        #endregion
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                
              
                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeResults(manager.StageValue);
                    
                    if (result)
                    {
                        
                        DOVirtual.DelayedCall(1, () =>
                        {
                            CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                            InputSignals.Instance.onEnableInput?.Invoke();
                            UISignals.Instance.onSetPercantageValue?.Invoke((byte)CoreGameSignals.Instance
                                .onGetCollectedObjectValue?.Invoke());

                        });

                    }
                    else
                    {
                        
                        
                        CoreGameSignals.Instance.onLevelFail?.Invoke();
                    }
                });
                return;
            }

            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                return;

            }
           

            if (other.CompareTag(_miniGame))
            {
                if (CoreGameSignals.Instance.onWhichMiniGameAreaEntered?.Invoke() == 1)
                { 
                    Debug.Log("Entered 1");
                    CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                    CoreGameSignals.Instance.onMiniGameAreaEntered?.Invoke((short)CoreGameSignals.Instance.onGetCollectedObjectValue?.Invoke());
                    InputSignals.Instance.onDisableInput?.Invoke();
                
                    DOVirtual.DelayedCall(3,() =>
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                        isEnded = true;
                        
                        DOVirtual.DelayedCall((float)CoreGameSignals.Instance.onGetCollectedObjectValue?.Invoke() / 10, () =>
                        {    
                            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                        

                        });
                    
                    });

                   
                    
                }
                   
                
                else
                { 
                    Debug.Log("Entered 0 ");
                    CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                    CoreGameSignals.Instance.onMiniGameAreaEntered?.Invoke((short)CoreGameSignals.Instance.onGetCollectedObjectValue?.Invoke());
                    InputSignals.Instance.onDisableInput?.Invoke();
                    DOVirtual.DelayedCall(3,() =>
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageValue);
                        InputSignals.Instance.onEnableInput?.Invoke();
                        isEnded = true;
                       
                        
                        
                       
                       
                    
                    });
                }
               
                   
                
              
            }
            if (isEnded)
            {
               
                foreach (var item in dic)
                {
                    if (item.Key == null) return;
                    
                
                    if (other.transform == item.Key)
                    {
                        Debug.Log(_multiplyValue);
                        _multiplyValue = item.Value;
                        if(CoreGameSignals.Instance.onWhichMiniGameAreaEntered?.Invoke() == 0)
                            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                             
                                    
                        

                    }
                

                }
                
            }

            
            
        }
        public void SetMultiplier(Dictionary<Transform, float> newDictionary)
        {
            dic = newDictionary;
            
        }
        public float GetMultiplyValue()
        {
            return _multiplyValue;
        }


        public void OnReset()
        {
            
        }

        
            
            
            
        }
    
        
    }

