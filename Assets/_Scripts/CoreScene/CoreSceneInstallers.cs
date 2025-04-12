using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using Zenject;

public class CoreSceneInstallers : MonoInstaller
{
    public LetSpawner LetSpawner;

    public LevelsData LevelsData;
    
    public override void InstallBindings()
    {
        //Container.Bind<GroundModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelsData>().FromInstance(LevelsData).AsSingle();
        //Container.Bind<LetSpawner>().FromInstance(LetSpawner).AsSingle();
        Container.Bind<PlayerMoveController>().FromComponentInHierarchy().AsSingle();
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
    }

    private void UseLevelControllers()
    {
        Container.Bind<LevelModel>().AsSingle();
        Container.Bind<LevelStarter>().FromComponentInHierarchy().AsSingle();
    }
}
