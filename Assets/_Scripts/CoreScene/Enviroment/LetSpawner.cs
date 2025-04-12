using System;
using _Scripts.CoreScene;
using _Scripts.CoreScene.Enviroment;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LetSpawner: MonoBehaviour
{
    private LevelModel m_levelModel;
    private IInstantiator m_instantiator;
    private LineHandler m_lineHandler;
    private bool m_isSpawning;

    public GameObject barricadePrefab;

    [Inject]
    public void Construct(LevelModel levelModel, IInstantiator instantiator, LineHandler lineHandler)
    {
        m_levelModel = levelModel;
        m_instantiator = instantiator;
        m_lineHandler = lineHandler;
    }
    
    public async UniTask StartSpawnBarricade()
    {
        if (m_isSpawning == true)
        {
            return;
        }
        
        m_isSpawning = true;
        while (m_isSpawning)
        {
            Transform line = m_lineHandler.BarricadeLines[Random.Range(0, m_lineHandler.BarricadeLines.Count)];

            m_instantiator.InstantiatePrefab(barricadePrefab, line.position, quaternion.identity, line.transform);

            await UniTask.Delay(m_levelModel.SpawnBaricadesDelay * 1000);
        }
    }
    
    public void StopSpawnBarricade() => m_isSpawning = false;
    
    
}
