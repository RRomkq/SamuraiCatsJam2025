using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CoreSceneInstallers : MonoInstaller
{
    public PlayerMoveController PlayerMoveController;
    public LetSpawner LetSpawner;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerMoveController>().FromInstance(PlayerMoveController).AsSingle();
        Container.Bind<LetSpawner>().FromInstance(LetSpawner).AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<GroundController>().FromComponentInHierarchy().AsSingle();
    }
}
