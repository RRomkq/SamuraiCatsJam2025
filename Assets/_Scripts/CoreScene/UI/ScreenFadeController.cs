using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CoreScene
{
    public class ScreenFadeController: MonoBehaviour
    {
        public Image m_image;
        
        private float m_targetAlpha;
        private float m_time;
        private Action m_onComplete;
        private bool m_isFading;

        public void AlphaTo(float alpha, float time)
        {
            m_targetAlpha = alpha;
            m_time = time;
        }
        
        public void AlphaToAndDoAction(float alpha, float time, Action onComplete)
        {
            m_targetAlpha = alpha;
            m_time = time;
            m_onComplete = onComplete;
            m_isFading = true;
        }

        private void Update()
        {
            if (m_time > 0)
            {
                float time = 1 - (m_time - Time.deltaTime)/ m_time;
                m_time -= Time.deltaTime;
                m_image.color = Color.Lerp(m_image.color, new Color(m_image.color.r, m_image.color.g, m_image.color.b, m_targetAlpha), time);
                return;
            }

            if (m_isFading)
            {
                m_onComplete?.Invoke();
            }

            m_isFading = false;
        }
    }
}