using _Scripts.CoreScene.Enviroment;
using _Scripts.CoreScene.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Scripts.CoreScene
{
    public class LevelStarter: MonoBehaviour
    {
        public Transform StartPlayerPosition;
        
        private PlayerMoveController m_playerMoveController;
        private ScreenFadeController m_screenFadeController;
        private LevelModel m_levelModel;
        private LevelsData m_levelsData;
        private GameManager m_gameManager;
        private PirsController m_pirsController;
        private FinishPirsController m_finishPirsController;
        private PlayerMoneyController m_playerMoneyController;
        private StartButtonController m_startButtonController;
        private PlayerMoneyModel m_playerMoneyModel;
        private PassengerOnBoardModel m_passengerOnBoardModel;
        private BoardResController m_boardResController;

        private CameraController m_cameraController;
        
        private int m_currentLevelIndex = 0;

        private LevelGhostsRepository m_ghostsRepository;
        
        [Inject]
        public void Construct(PlayerMoveController playerMoveController,
            ScreenFadeController screenFadeController,
            LevelModel levelModel,
            LevelsData levelsData,
            GameManager gameManager,
            PirsController pirsController,
            FinishPirsController finishPirsController,
            PlayerMoneyController playerMoneyController,
            StartButtonController startButtonController,
            PlayerMoneyModel playerMoneyModel,
            PassengerOnBoardModel passengerOnBoardModel,
            CameraController cameraController,
            BoardResController boardResController,
            LevelGhostsRepository ghostsRepository
            )
        {
            m_playerMoveController = playerMoveController;
            m_screenFadeController = screenFadeController;
            m_levelModel = levelModel;
            m_levelsData = levelsData;
            m_gameManager = gameManager;
            m_pirsController = pirsController;
            m_finishPirsController = finishPirsController;
            m_playerMoneyController = playerMoneyController;
            m_startButtonController = startButtonController;
            m_playerMoneyModel = playerMoneyModel;
            m_passengerOnBoardModel = passengerOnBoardModel;
            m_cameraController = cameraController;
            m_ghostsRepository = ghostsRepository;
            m_boardResController = boardResController;
        }

        public void Awake()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            m_levelModel.SetLevelSettings(m_levelsData.LevelSettingsMap[m_levelModel.NeedMoneyForPassenger.FindLastIndex(needMoney => needMoney <= m_playerMoneyModel.Money)]);
            m_playerMoveController.SetBoard((int)m_levelModel.DifficultyLevel);
            
            m_playerMoveController.GoToTargetInstant(StartPlayerPosition);
            m_screenFadeController.AlphaTo(1, 0);
            
            m_gameManager.IsLastBarricadeComplete = false;
            
            m_pirsController.ResetPirs();
            m_finishPirsController.ResetPirs();
            
            StartLevelAsync().Forget();
        }

        private async UniTask StartLevelAsync()
        {
            m_cameraController.ResetCamera();
            
            await UniTask.DelayFrame(1);

            m_ghostsRepository.SpawnGhosts(4);
            
            m_playerMoveController.GoToFirstLine(() => AnimateMoneyAndShowStartButton().Forget());
            m_screenFadeController.AlphaTo(0, 2);
            m_boardResController.Show();
            
            m_cameraController.MoveToStartPoint();
        }

        public async UniTask AnimateMoneyAndShowStartButton()
        {
            int passengersCount =
                m_levelModel.NeedMoneyForPassenger.FindIndex(needMoney => needMoney > m_playerMoneyModel.Money) - 1;

            m_passengerOnBoardModel.MaxPassengersOnBoard = passengersCount;
            m_passengerOnBoardModel.PassengersCount = passengersCount;
            
            await m_playerMoneyController
                .AddMoneyOnBoard(passengersCount * 2);
            m_startButtonController.Show();
        }
    }
}