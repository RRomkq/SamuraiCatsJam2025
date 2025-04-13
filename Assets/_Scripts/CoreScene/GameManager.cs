using System;
using _Scripts.CoreScene.Enviroment;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.CoreScene
{
    public class GameManager: IInitializable
    {
        private LevelModel m_levelModel;

        private DateTime m_finishedTime;
        
        private ScreenFadeController m_screenFadeController;
        private LetSpawner m_letSpawner;
        private PlayerMoveController m_playerMoveController;
        private FinishPirsController m_finishPirsController;
        // private PlayerMoneyController m_playerMoneyController;
        // private FinalLevelWindowController m_finalLevelWindowController;
        
        public ShipState ShipState { get; set; }
        public bool IsLastBarricadeComplete { get; set; } = false;
        
        public GameManager(LevelModel levelModel,
            LetSpawner letSpawner,
            PlayerMoveController playerMoveController,
            FinishPirsController finishPirsController//,
            // PlayerMoneyController playerMoneyController,
            //FinalLevelWindowController finalLevelWindowController
            )
        {
            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
            m_playerMoveController = playerMoveController;
            m_finishPirsController = finishPirsController;
            // m_playerMoneyController = playerMoneyController;
            // m_finalLevelWindowController = finalLevelWindowController;
        }
        
        public DateTime FinishedTime => m_finishedTime;
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
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
            m_playerMoveController.GoToLastLine();
            m_finishPirsController.GoPirsToVisionPosition(() =>
            {
               FinishLevelAsync().Forget();
            });
        }

        public async UniTask FinishLevelAsync()
        {
            ShipState = ShipState.Mooring;
            
            // m_finalLevelWindowController.Show();
        }
    }
}