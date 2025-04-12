using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using Zenject;

public class CoreSceneInstallers : MonoInstaller
{
    public PlayerMoveController PlayerMoveController;
    public LetSpawner LetSpawner;

    public LevelsData LevelsData;
    
    public override void InstallBindings()
    {
        Container.Bind<GroundModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelsData>().FromInstance(LevelsData).AsSingle();
        Container.Bind<LetSpawner>().FromInstance(LetSpawner).AsSingle();
        Container.Bind<PlayerMoveController>().FromInstance(PlayerMoveController).AsSingle();
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        Container.Bind<SpeedService>().AsSingle();
        Container.Bind<ScreenFadeController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LevelModel>().AsSingle();
        Container.Bind<LevelStarter>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<PassengerQ>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PassengerSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PirsController>().FromComponentInHierarchy().AsSingle();
    }
}
