using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene.Enviroment
{
    public class EnvironmentMover: MonoBehaviour
    {
        private LineHandler m_lineHandler;
        private GameManager m_gameManager;
        private LevelModel m_levelModel;

        [Inject]
        private void Construct(LineHandler lineHandler, LevelModel levelModel, GameManager gameManager)
        {
            m_lineHandler = lineHandler;
            m_levelModel = levelModel;
            m_gameManager = gameManager;
        }
        
        private void Update()
        {
            if (m_gameManager.ShipState == ShipState.Swimming)
            {
                transform.position += m_lineHandler.DirectionToHarold * m_levelModel.Speed * Time.deltaTime;
            }
        }
    }
}