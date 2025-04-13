using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class AvitoWindowController: MonoBehaviour
    {
        private GameSceneManager m_gameSceneManager;
        
        [Inject]
        public void Construct(GameSceneManager gameSceneManager)
        {
            m_gameSceneManager = gameSceneManager;
        }
        
        public void LoadFinalScene()
        {
            m_gameSceneManager.GotoNextScene();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}