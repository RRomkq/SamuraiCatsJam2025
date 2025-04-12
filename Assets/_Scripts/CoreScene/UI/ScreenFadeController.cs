using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CoreScene
{
    public class ScreenFadeController: MonoBehaviour
    {
        public Image m_image;

        public void AlphaTo(float alpha, float time)
        {
            m_image.DOFade(alpha, time);
        }
        
        public void AlphaToAndDoAction(float alpha, float time, Action onComplete)
        {
            m_image.DOFade(alpha, time)
                .SetEase(Ease.Linear)
                .OnComplete(onComplete.Invoke);
        }
        
    }
}