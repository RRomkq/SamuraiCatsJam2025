using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.CoreScene
{
    public class ClickerByCircle: MonoBehaviour, IDisposable
    {
        public Image m_circle;
        public GameObject Parent;
        public GameObject SpaceText;
        private int m_needClickCount;
        private int m_currentClick;
        private PlayerMoveController m_playerMoveController;
        private GameManager m_gameManager;
        private CancellationTokenSource m_cts;

        public AudioSource ClickAudio;
        
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

        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && m_cts != null)
            {
                OnClick();
            }
        }

        public event Action<bool> FinishClickEvent;
        
        public void StartClickEvent(int needClickCount)
        {
            Parent.SetActive(true);
            m_circle.fillAmount = (float)3 / (float)m_needClickCount;
            m_currentClick = 3;
            m_cts = new CancellationTokenSource();
            m_playerMoveController.Click += OnClick;
            ReduceProgres(m_cts.Token).Forget();
            m_needClickCount = needClickCount;
            
            AnimateClickSpaceText(m_cts.Token).Forget();
        }

        private void OnClick()
        {
            ClickAudio.Play();
            m_currentClick++;
            m_circle.fillAmount = (float) m_currentClick / m_needClickCount;

            if (m_currentClick == m_needClickCount)
            {
                Finish(true);
            }
        }

        private async UniTask AnimateClickSpaceText(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    SpaceText.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, elasticity: 0.5f);
                    await UniTask.Delay(500, cancellationToken: token);
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

        private async UniTask ReduceProgres(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    m_currentClick--;
                    m_circle.fillAmount = (float)m_currentClick / m_needClickCount;
                    await UniTask.Delay(800, cancellationToken: token);

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

            if (m_cts != null)
            {
                m_cts.Cancel();
                m_cts.Dispose();
                m_cts = null;
            }
        }

        public void Dispose()
        {
            m_cts?.Dispose();
            m_gameManager.LevelFinished -= OnLevelFinished;
        }
    }
}