using System;
using _Scripts.CoreScene;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LetSpawner : MonoBehaviour
{
    public GameObject objectPrefab;      // Префаб объекта для спавна
    private float spawnInterval = 1f;     // Интервал между спавнами
    
    public Transform LeftSpawnBorder;
    public Transform RightSpawnBorder;
    
    private IInstantiator m_instantiator;
    private GameManager m_gameManager;
    private LevelModel m_levelModel;

    private float timer = 0f;

    [Inject]
    public void Construct(IInstantiator instantiator, GameManager gameManager, LevelModel levelModel)
    {
        m_instantiator = instantiator;
        m_gameManager = gameManager;

        m_levelModel = levelModel;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (m_gameManager.ShipState != ShipState.Swimming)
        {
            return;
        }

        if (m_gameManager.FinishedTime - DateTime.Now <= TimeSpan.FromSeconds(5))
        {
            return;
        }

        if (timer >= m_levelModel.SpawnBaricadesDelay)
        {
            timer = 0f;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, Random.Range(LeftSpawnBorder.position.z, RightSpawnBorder.position.z));

        GameObject newObj = m_instantiator.InstantiatePrefab(objectPrefab, spawnPos, Quaternion.identity, transform);
    }
}
