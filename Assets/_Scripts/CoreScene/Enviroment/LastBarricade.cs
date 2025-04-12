using System;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class LastBarricade: MonoBehaviour
    {
        private GameManager m_gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager)
        {
            m_gameManager = gameManager;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !m_gameManager.IsLastBarricadeComplete)
            {
                m_gameManager.IsLastBarricadeComplete = true;
                m_gameManager.FinishLevel();
            }
        }
    }
}