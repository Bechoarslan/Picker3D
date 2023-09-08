using System;
using DG.Tweening;
using RunTime.Data.ValueObjects;
using TMPro;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confettiParticle;



        #endregion

        #region Private Variables

        private PlayerMeshData _data;



        #endregion

        #endregion


        

        internal void SetMeshData(PlayerMeshData scaleData)
        {
            _data = scaleData;
        }

        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScale(_data.ScaleCounter, 1).SetEase(Ease.Flash);
        }

        internal void ShowText()
        {
            scaleText.DOFade(1, 0f).SetEase(Ease.Flash).OnComplete(() => scaleText.DOFade(0, 0).SetDelay(0.65f));

            scaleText.rectTransform.DOAnchorPosY(.85f, .65f).SetRelative(true).SetEase(Ease.OutBounce).OnComplete(() =>
                scaleText.rectTransform.DOAnchorPosY(-.85f, .65f).SetRelative(true));
        }

        internal void PlayConfettiParticle()
        {
            confettiParticle.Play();
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }

       
    }
}