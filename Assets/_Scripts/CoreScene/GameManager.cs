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
        private FinalLevelWindowController m_finalLevelWindowController;
        private PlayerMoneyModel m_playerMoneyModel;
        private AvitoWindowController m_avitoWindowController;
        private CameraController m_cameraController;

        private LevelGhostsRepository m_ghostsRepository;

        private GroundController m_groundController;
        private GameModel m_gameModel;

        public ShipState ShipState
        {
            get => m_gameModel.ShipState;
            set => m_gameModel.ShipState = value;
        }

        public bool IsLastBarricadeComplete { get; set; } = false;

        private GhostStartTransferManager m_ghostsTransferManager;
        private HelpImageController m_helpImageController;
        
        public GameManager(LevelModel levelModel,
            LetSpawner letSpawner,
            PlayerMoveController playerMoveController,
            FinalLevelWindowController finalLevelWindowController,
            PlayerMoneyModel playerMoneyModel,
            AvitoWindowController avitoWindowController,
            LevelGhostsRepository ghostsRepository,
            CameraController cameraController,
            GroundController groundController,
            GhostStartTransferManager ghostsTransferManager,
            HelpImageController helpImageController,
            GameModel gameModel)
        {
            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
            m_playerMoveController = playerMoveController;
            m_finalLevelWindowController = finalLevelWindowController;
            m_playerMoneyModel = playerMoneyModel;
            m_avitoWindowController = avitoWindowController;
            m_cameraController = cameraController;
            m_ghostsRepository = ghostsRepository;
            m_groundController = groundController;
            m_ghostsTransferManager = ghostsTransferManager;
            m_gameModel = gameModel;
            m_helpImageController = helpImageController;
        }
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
        }
        
        public async void StartSwimming()
        {
            await m_ghostsTransferManager.TransferGhostsFromStartToBoat();
         
            m_helpImageController.Show();
            
            ShipState = ShipState.Swimming;
            
            m_groundController.StartGroundMove();
            m_letSpawner.StartSpawnBarricade().Forget();
            
            m_playerMoveController.GoToCenterLine();
            
            m_cameraController.MoveToSwimMode();
            LevelStarted?.Invoke();
        }
        
        public void FinishLevel()
        {
            LevelFinished?.Invoke();
            m_playerMoveController.GoToLastLine(() =>
            {
                FinishLevelAsync();
            });
            m_cameraController.MoveToFinishPoint();
        }

        private void FinishLevelAsync()
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
            
            m_helpImageController.Hide();
            
            m_ghostsRepository.DisposeGhosts();
        }
    }
}