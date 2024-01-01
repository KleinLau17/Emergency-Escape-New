using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private LivingEntity playerEntity;

    public Wave[] waves;
    private Wave _currentWave;
    private int _currentWaveIndex;

    private int _enemyRemainingAliveCount;
         
    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            return;
        }

        if (playerEntity == null)
        {
            return;
        }

        playerEntity.OnDeath += OnPlayerDeath;

        StartCoroutine(NextWaveCoroutine());
    }

    private IEnumerator NextWaveCoroutine()
    {
        _currentWaveIndex++;
        if (_currentWaveIndex - 1 < waves.Length)
        {
            _currentWave = waves[_currentWaveIndex - 1];
            _enemyRemainingAliveCount = _currentWave.count;

            for (int i = 0; i < _currentWave.count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Beetle beetle = Instantiate(_currentWave.beetle, spawnPoints[spawnIndex].position, Quaternion.identity);
                beetle.target = playerEntity.transform;
                beetle.OnDeath += OnEnemyDeath;
                yield return new WaitForSeconds(_currentWave.timeBetweenSpawn);
            }
        }
    }

    private void OnEnemyDeath()
    {
        _enemyRemainingAliveCount--;
        if (_enemyRemainingAliveCount == 0)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }

    private void OnPlayerDeath()
    {
        StopCoroutine(NextWaveCoroutine());
        Debug.Log("Game Over!!!");
    }
}
