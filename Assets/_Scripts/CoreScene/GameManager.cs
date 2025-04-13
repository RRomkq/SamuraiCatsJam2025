using System;
using _Scripts.CoreScene.Enviroment;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Scripts.CoreScene
{
    public class GameManager: IInitializable
    {
        public event Action LevelFinished;

        public event Action LevelStarted;
        
        private LevelModel m_levelModel;

        private DateTime m_finishedTime;
        
        private ScreenFadeController m_screenFadeController;
        private LetSpawner m_letSpawner;
        private PlayerMoveController m_playerMoveController;
        private FinishPirsController m_finishPirsController;
        private PlayerMoneyController m_playerMoneyController;
        private FinalLevelWindowController m_finalLevelWindowController;
        private PlayerMoneyModel m_playerMoneyModel;
        private AvitoWindowController m_avitoWindowController;
        private CameraController m_cameraController;
        private StartLevelGhostsRepository m_ghostsRepository;
        
        public ShipState ShipState { get; set; }
        public bool IsLastBarricadeComplete { get; set; } = false;
        
        public GameManager(LevelModel levelModel,
            LetSpawner letSpawner,
            PlayerMoveController playerMoveController,
            FinishPirsController finishPirsController,
            PlayerMoneyController playerMoneyController,
            FinalLevelWindowController finalLevelWindowController,
            PlayerMoneyModel playerMoneyModel,
            AvitoWindowController avitoWindowController,
            StartLevelGhostsRepository ghostsRepository,
            CameraController cameraController)
        {
            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
            m_playerMoveController = playerMoveController;
            m_finishPirsController = finishPirsController;
            m_playerMoneyController = playerMoneyController;
            m_finalLevelWindowController = finalLevelWindowController;
            m_playerMoneyModel = playerMoneyModel;
            m_avitoWindowController = avitoWindowController;
            m_cameraController = cameraController;
            m_ghostsRepository = ghostsRepository;
        }
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
        }
        
        public void StartSwimming()
        {
            ShipState = ShipState.Swimming;
            m_letSpawner.StartSpawnBarricade().Forget();
            
            m_playerMoveController.GoToCenterLine();
            
            m_cameraController.MoveToSwimMode();
            LevelStarted?.Invoke();
        }
        
        public void FinishLevel()
        {
            LevelFinished?.Invoke();
            m_playerMoveController.GoToLastLine();
            m_finishPirsController.GoPirsToVisionPosition(() =>
            {
                FinishLevelAsync();
            });
            
            m_cameraController.MoveToFinishPoint();
        }

        public void FinishLevelAsync()
        {
            ShipState = ShipState.Mooring;
            
            if (m_playerMoneyModel.Money + m_playerMoneyModel.MoneyOnBoard >= m_levelModel.WinMoney)
            {
                m_avitoWindowController.Show();
            }
            else
            {
                m_finalLevelWindowController.Show();
            }
            
            m_ghostsRepository.DisposeGhosts();
        }
    }
}