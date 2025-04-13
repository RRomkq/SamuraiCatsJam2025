using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using _Scripts.CoreScene.Player;
using Zenject;

public class CoreSceneInstallers : MonoInstaller
{
    public LetSpawner LetSpawner;

    public LevelsData LevelsData;
    public FeedbackSO FeedbackSO;
    public GlobalGameSettings GlobalGameSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelsData>().FromInstance(LevelsData).AsSingle();
        Container.Bind<PlayerMoveController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerMoneyController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerMoneyModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.Bind<ScreenFadeController>().FromComponentInHierarchy().AsSingle();
        
        UseLevelControllers();

        //Container.Bind<PassengerQ>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<PassengerSpawner>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<PirsController>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<LineHandler>().FromComponentInHierarchy().AsSingle();

        Container.Bind<LetSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PirsController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FinishPirsController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ClickerByCircle>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FeedbackSO>().FromInstance(FeedbackSO).AsSingle();
        Container.Bind<FinalLevelWindowController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PassengerOnBoardModel>().AsSingle();
        Container.Bind<AvitoWindowController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GlobalGameSettings>().FromInstance(GlobalGameSettings).AsSingle();
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
        Container.BindInterfacesAndSelfTo<ClickerEventController>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<GroundController>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<GameModel>().AsSingle();
    }

    private void UseLevelControllers()
    {
        Container.Bind<StartButtonController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelModel>().AsSingle();
        Container.Bind<LevelStarter>().FromComponentInHierarchy().AsSingle();
    }
}
