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

    public GameObject barricadePrefab;
    public GameObject lastBarricadePrefab;
    

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
            int index = Random.Range(0, m_lineHandler.BarricadeLines.Count);
            for (int i = 0; i < m_lineHandler.BarricadeLines.Count; i++)
            {
                emptyIndexes[i] = i == index ? 1 : 0;
            }
        }
        else if (m_levelModel.DifficultyLevel == DifficultyLevel.Medium)
        {
            List<int> numbers = new List<int>() { 0, 1, 2, 3, 4 };
            int wallCount = Random.Range(2, 4);
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
        else
        {
            int index = Random.Range(0, m_lineHandler.BarricadeLines.Count);
            for (int i = 0; i < m_lineHandler.BarricadeLines.Count; i++)
            {
                emptyIndexes[i] = i == index ? 0 : 1;
            }
        }

        for(int i = 0; i < emptyIndexes.Count; i++)
        {
            if (emptyIndexes[i] == 1)
            {
                Transform line = m_lineHandler.BarricadeLines[i];

                m_instantiator.InstantiatePrefab(barricadePrefab, line.position, quaternion.identity, line.transform);
            }
        }
        
    }
    
    public void StopSpawnBarricade() => m_isSpawning = false;
    
    
}
