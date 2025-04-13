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
        private FinalLevelWindowController m_finalLevelWindowController;
        private PlayerMoneyModel m_playerMoneyModel;
        private AvitoWindowController m_avitoWindowController;
        private CameraController m_cameraController;
        private GroundController m_groundController;
        
        public ShipState ShipState { get; set; }
        public bool IsLastBarricadeComplete { get; set; } = false;
        
        public GameManager(LevelModel levelModel,
            LetSpawner letSpawner,
            PlayerMoveController playerMoveController,
            FinalLevelWindowController finalLevelWindowController,
            PlayerMoneyModel playerMoneyModel,
            AvitoWindowController avitoWindowController,
            CameraController cameraController,
            GroundController groundController)
        {
            m_levelModel = levelModel;
            m_letSpawner = letSpawner;
            m_playerMoveController = playerMoveController;
            m_finalLevelWindowController = finalLevelWindowController;
            m_playerMoneyModel = playerMoneyModel;
            m_avitoWindowController = avitoWindowController;
            m_cameraController = cameraController;
            m_groundController = groundController;
        }
        
        public void Initialize()
        {
            ShipState = ShipState.Mooring;
        }
        
        public void StartSwimming()
        {
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
                FinishLevelAsync().Forget();
            });
            m_cameraController.MoveToFinishPoint();
        }

        public async UniTask FinishLevelAsync()
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
        }

        public event Action LevelFinished;

        public event Action LevelStarted;
    }
}