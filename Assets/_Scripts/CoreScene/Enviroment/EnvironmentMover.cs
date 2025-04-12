using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class EnvironmentMover: MonoBehaviour
    {
        private GameManager m_gameManager;
        private SpeedService m_speedService;

        [Inject]
        private void Construct(GameManager gameManager, SpeedService speedService)
        {
            m_gameManager = gameManager;
            m_speedService = speedService;
        }
        
        private void Update()
        {
            if (m_gameManager.ShipState == ShipState.Swimming)
            {
                transform.position += m_speedService.CountSpeed();
            }
        }
    }
}