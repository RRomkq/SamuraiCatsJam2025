using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene
{
    public class ClickerByCircle: MonoBehaviour
    {
        public Image m_circle;
        private int m_needClickCount;
        private int m_currentClick;
        private PlayerMoveController m_playerMoveController;
        private CancellationTokenSource m_cts;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController)
        {
            m_playerMoveController = playerMoveController;
        }
        
        public void StartClickEvent(int needClickCount)
        {
            m_circle.fillAmount = 0;
            m_currentClick = 0;
            m_cts = new CancellationTokenSource();
            m_playerMoveController.Click += OnClick;
            ReduceProgres(m_cts.Token).Forget();
            m_needClickCount = needClickCount;
            
            gameObject.SetActive(true);
        }

        private void OnClick()
        {
            m_currentClick++;
            m_circle.fillAmount = (float) m_currentClick / m_needClickCount;

            if (m_currentClick == m_needClickCount)
            {
                m_playerMoveController.Click -= OnClick;
                gameObject.SetActive(false);
                m_cts.Cancel();
                m_cts.Dispose();
            }
        }

        private async UniTask ReduceProgres(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    await UniTask.Delay(500, cancellationToken:token);
                    m_currentClick--;
                    m_circle.fillAmount = (float) m_currentClick / m_needClickCount;
                }
            }
            catch (OperationCanceledException) {}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}