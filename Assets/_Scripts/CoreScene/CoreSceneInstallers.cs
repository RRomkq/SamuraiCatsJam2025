using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using Zenject;

public class CoreSceneInstallers : MonoInstaller
{
    public PlayerMoveController PlayerMoveController;
    public LetSpawner LetSpawner;
    public GroundController StartGroundController;
    public GroundController FinishGroundController;

    public LevelSettings LevelSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelSettings>().FromInstance(LevelSettings).AsSingle();
        Container.Bind<GroundModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LetSpawner>().FromInstance(LetSpawner).AsSingle();
        Container.Bind<PlayerMoveController>().FromInstance(PlayerMoveController).AsSingle();
        Container.Bind<GameManager>().AsSingle();
    }
}
