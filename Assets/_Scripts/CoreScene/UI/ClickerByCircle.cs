using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene
{
    public class ClickerByCircle: MonoBehaviour, IDisposable
    {
        public Image m_circle;
        public GameObject Parent;
        private int m_needClickCount;
        private int m_currentClick;
        private PlayerMoveController m_playerMoveController;
        private GameManager m_gameManager;
        private CancellationTokenSource m_cts;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController, GameManager gameManager)
        {
            m_playerMoveController = playerMoveController;
            m_gameManager = gameManager;
            m_gameManager.LevelFinished += OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            m_currentClick = -100;
        }
        
        public event Action<bool> FinishClickEvent;
        
        public void StartClickEvent(int needClickCount)
        {
            m_circle.fillAmount = (float)3 / m_needClickCount;
            m_currentClick = 3;
            m_cts = new CancellationTokenSource();
            m_playerMoveController.Click += OnClick;
            ReduceProgres(m_cts.Token).Forget();
            m_needClickCount = needClickCount;
            
            Parent.SetActive(true);
        }

        private void OnClick()
        {
            m_currentClick++;
            m_circle.fillAmount = (float) m_currentClick / m_needClickCount;

            if (m_currentClick == m_needClickCount)
            {
                Finish(true);
            }
        }

        private async UniTask ReduceProgres(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    await UniTask.Delay(800, cancellationToken: token);
                    m_currentClick--;
                    m_circle.fillAmount = (float)m_currentClick / m_needClickCount;

                    if (m_currentClick <= 0)
                    {
                        Finish(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        private void Finish(bool result)
        {
            FinishClickEvent?.Invoke(result);
            m_playerMoveController.Click -= OnClick;
            Parent.SetActive(false);
            m_cts.Cancel();
            m_cts.Dispose();
        }

        public void Dispose()
        {
            m_cts?.Dispose();
            m_gameManager.LevelFinished -= OnLevelFinished;
        }
    }
}