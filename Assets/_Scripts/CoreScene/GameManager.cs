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
        
        public ShipState ShipState { get; set; }
        
        public GameManager(GroundModel groundModel, LevelModel levelModel, ScreenFadeController screenFadeController)
        {
            m_startGroundController = groundModel.StartGroundController;
            m_finishGroundController = groundModel.FinishGroundController;

            m_levelModel = levelModel;
            m_screenFadeController = screenFadeController;
        }
        
        public DateTime FinishedTime => m_finishedTime;
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
            m_startGroundController.GoToVisionPosition();
        }
        
        public void StartSwimming()
        {
            m_startGroundController.GoToNotVisionPosition();
            ShipState = ShipState.Swimming;

            WaitAndFinishLevel().Forget();
        }

        private async UniTask WaitAndFinishLevel()
        {
            m_finishedTime = DateTime.Now.AddSeconds(m_levelModel.LevelDurationInSeconds);
            await UniTask.Delay(m_levelModel.LevelDurationInSeconds * 1000);
            FinishSwimming();
        }

        public void FinishSwimming()
        {
            m_finishGroundController.GoToVisionPosition();
            ShipState = ShipState.Mooring;
        }
    }
}