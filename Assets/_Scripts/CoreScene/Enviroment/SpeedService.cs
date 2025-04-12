using UnityEngine;

namespace _Scripts.CoreScene.Enviroment
{
    public class SpeedService
    {
        private readonly PlayerMoveController m_playerMoveController;
        private readonly LevelModel m_levelModel;

        public SpeedService(PlayerMoveController playerMoveController, LevelModel levelModel)
        {
            m_playerMoveController = playerMoveController;
            m_levelModel = levelModel;
        }
        
        public Vector3 CountSpeed()
        {
            float slowdownFactor = Mathf.Min((Mathf.Abs(m_playerMoveController.CurrentYaw) / m_playerMoveController.maxTurnAngle), m_levelModel.MaxSlowdownFactor);

            return Vector3.left * ((1 - slowdownFactor) * m_levelModel.Speed * Time.deltaTime);
        }
    }
}