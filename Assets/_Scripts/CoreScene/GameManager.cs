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
        
        public ShipState ShipState { get; set; }
        
        public GameManager(LevelModel levelModel, LetSpawner letSpawner)
        {
            //m_startGroundController = groundModel.StartGroundController;
           // m_finishGroundController = groundModel.FinishGroundController;

            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
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
            //m_startGroundController.GoToNotVisionPosition();
            ShipState = ShipState.Swimming;
            m_letSpawner.StartSpawnBarricade().Forget();

            WaitAndFinishLevel().Forget();
        }

        private async UniTask WaitAndFinishLevel()
        {
            m_finishedTime = DateTime.Now.AddSeconds(m_levelModel.LevelDurationInSeconds);
        }
        
        public void FinishLevel()
        {
            ShipState = ShipState.Mooring;
        }
    }
}