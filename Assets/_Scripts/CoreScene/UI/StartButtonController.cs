using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class StartButtonController: MonoBehaviour
    {
        private GameManager m_gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            m_gameManager = gameManager;
        }
        
        public void StartGame() 
        {
            m_gameManager.StartSwimming();
            gameObject.SetActive(false);
        }
        
        public void Show() => gameObject.SetActive(true);
    }
}