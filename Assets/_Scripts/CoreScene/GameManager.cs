using System;
using _Scripts.CoreScene.Enviroment;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.CoreScene
{
    public class GameManager: IInitializable
    {
        private bool m_isTransports;
        
        private LevelSettings m_levelSettings;

        private DateTime m_finishedTime;
        
        private GroundController m_startGroundController;
        private GroundController m_finishGroundController;
        
        public GameManager(GroundModel groundModel, LevelSettings levelSettings)
        {
            m_startGroundController = groundModel.StartGroundController;
            m_finishGroundController = groundModel.FinishGroundController;

            m_levelSettings = levelSettings;
        }
        
        public DateTime FinishedTime => m_finishedTime;
        
        public void Initialize()
        {
            
        }
        
        public bool IsTransports => m_isTransports;

        public event Action StartTransports;
        
        public event Action FinishTransports;

        public void StartSwimming()
        {
            m_isTransports = true;
            m_startGroundController.GoToNotVisionPosition();
            StartTransports?.Invoke();

            WaitAndFinishLevel().Forget();
        }

        private async UniTask WaitAndFinishLevel()
        {
            m_finishedTime = DateTime.Now.AddSeconds(m_levelSettings.LevelDurationInSeconds);
            await UniTask.Delay(m_levelSettings.LevelDurationInSeconds * 1000);
            FinishSwimming();
        }

        public void FinishSwimming()
        {
            m_finishGroundController.GoToVisionPosition();
            FinishTransports?.Invoke();
        }
    }
}