using System;
using _Scripts.CoreScene.Enviroment;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.CoreScene
{
    public class GameManager: IInitializable
    {
        private LevelModel m_levelModel;

        private DateTime m_finishedTime;
        
        private GroundController m_startGroundController;
        private GroundController m_finishGroundController;
        private ScreenFadeController m_screenFadeController;
        private LetSpawner m_letSpawner;
        private PlayerMoveController m_playerMoveController;
        
        public ShipState ShipState { get; set; }
        public bool IsLastBarricadeComplete { get; set; } = false;
        
        public GameManager(LevelModel levelModel, LetSpawner letSpawner, PlayerMoveController playerMoveController)
        {
            //m_startGroundController = groundModel.StartGroundController;
           // m_finishGroundController = groundModel.FinishGroundController;

            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
            m_playerMoveController = playerMoveController;
            //m_screenFadeController = screenFadeController;
        }
        
        public DateTime FinishedTime => m_finishedTime;
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
            //m_startGroundController.GoToVisionPosition();
        }
        
        public void StartSwimming()
        {
            ShipState = ShipState.Swimming;
            m_letSpawner.StartSpawnBarricade().Forget();
            
            m_playerMoveController.GoToCenterLine();

            m_finishedTime = DateTime.Now.AddSeconds(m_levelModel.LevelDurationInSeconds);
        }
        
        public void FinishLevel()
        {
            ShipState = ShipState.Mooring;
            m_playerMoveController.GoToLastLine();
        }
    }
}