using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreScene;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LetSpawner : MonoBehaviour
{
    public GameObject objectPrefab;      // Префаб объекта для спавна
    public float spawnInterval = 1f;     // Интервал между спавнами
    public float minSpeed = 1f;          // Минимальная скорость
    public float maxSpeed = 5f;          // Максимальная скорость
    
    public Transform LeftSpawnBorder;
    public Transform RightSpawnBorder;
    
    private IInstantiator m_instantiator;
    private GameManager m_gameManager;

    private float timer = 0f;

    [Inject]
    public void Construct(IInstantiator instantiator, GameManager gameManager)
    {
        m_instantiator = instantiator;
        m_gameManager = gameManager;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (!m_gameManager.IsTransports)
        {
            return;
        }

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, Random.Range(LeftSpawnBorder.position.z, RightSpawnBorder.position.z));

        GameObject newObj = m_instantiator.InstantiatePrefab(objectPrefab, spawnPos, Quaternion.identity, transform);

        // Назначаем случайную скорость объекту
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        LetMover mover = newObj.GetComponent<LetMover>();
        if (mover != null)
        {
            mover.moveSpeed = randomSpeed;
        }
    }
}
