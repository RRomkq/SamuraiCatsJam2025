using System.Collections.Generic;
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

    public List<GameObject> barricadePrefab;
    public GameObject lastBarricadePrefab;

    public int CurrentBarricade { get; private set; }
    

    [Inject]
    public void Construct(LevelModel levelModel, IInstantiator instantiator, LineHandler lineHandler)
    {
        m_levelModel = levelModel;
        m_instantiator = instantiator;
        m_lineHandler = lineHandler;
    }
    
    public async UniTask StartSpawnBarricade()
    {
        for (int i = 0; i < m_levelModel.BarricadesCount; i++)
        {
            GenerateMaze();
            CurrentBarricade++;

            await UniTask.Delay(m_levelModel.SpawnBaricadesDelay * 1000);
        }

        foreach (var bTransform in m_lineHandler.BarricadeLines)
        {
            m_instantiator.InstantiatePrefab(lastBarricadePrefab, bTransform.position, quaternion.identity, bTransform);
        }
    }
    
    void GenerateMaze()
    {
        List<int> emptyIndexes = new List<int>(5) {0, 0, 0, 0, 0};

        if (m_levelModel.DifficultyLevel == DifficultyLevel.Easy)
        {
            RandomWall(emptyIndexes, 1, 3);
        }
        else if (m_levelModel.DifficultyLevel == DifficultyLevel.Medium)
        {
            RandomWall(emptyIndexes, 2, 4);
        }
        else
        {
            RandomWall(emptyIndexes, 3, 5);
        }

        for(int i = 0; i < emptyIndexes.Count; i++)
        {
            if (emptyIndexes[i] == 1)
            {
                Transform line = m_lineHandler.BarricadeLines[i];

                m_instantiator.InstantiatePrefab(barricadePrefab[Random.Range(0, barricadePrefab.Count)], line.position, quaternion.identity, line.transform);
            }
        }
        
    }

    private static void RandomWall(List<int> emptyIndexes, int minValue, int maxValue)
    {
        List<int> numbers = new List<int>() { 0, 1, 2, 3, 4 };
        int wallCount = Random.Range(minValue, maxValue);
        for (int i = 0; i < wallCount; i++)
        {
            int index = Random.Range(0, numbers.Count);
            emptyIndexes[index] = 1;
            numbers.Remove(index);
        }

        foreach (var index in numbers)
        {
            emptyIndexes[index] = 0;
        }
    }

    public void StopSpawnBarricade() => m_isSpawning = false;
    
    
}
