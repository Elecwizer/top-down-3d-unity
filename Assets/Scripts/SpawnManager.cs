using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Rock prefabs array which are assigned in inspector
    [SerializeField] private GameObject[] _rockPrefabs;

    // Spawn position ranges
    [SerializeField] private float _minX = -10f;
    [SerializeField] private float _maxX = 10f;
    [SerializeField] private float _spawnY = 3.5f;
    [SerializeField] private float _minZ = 45f;
    [SerializeField] private float _maxZ = 350f;

    // Spawn timing
    [SerializeField] private float _baseSpawnDelay = 3.0f;

    // Difficulty
    [SerializeField] private float _difficultyStepSeconds = 10f; // every 10 seconds
    [SerializeField] private int _maxDifficultyIncreases = 3;    // maximum 3 times

    private int _difficultyLevel = 0;
    private Coroutine _spawnRoutine;

    void Start()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
        StartCoroutine(DifficultyRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnOne();

            // Increase difficulty by reducing delay
            float delay = _baseSpawnDelay - (0.5f * _difficultyLevel);
            delay = Mathf.Max(0.2f, delay);

            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnOne()
    {
        if (_rockPrefabs == null || _rockPrefabs.Length == 0) return;

        int index = Random.Range(0, _rockPrefabs.Length);
        GameObject prefab = _rockPrefabs[index];

        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        Vector3 spawnPos = new Vector3(x, _spawnY, z);

        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }

    IEnumerator DifficultyRoutine()
    {
        // Increase difficulty every 10 seconds, max 3 times
        while (_difficultyLevel < _maxDifficultyIncreases)
        {
            yield return new WaitForSeconds(_difficultyStepSeconds);
            _difficultyLevel++;
            Debug.Log("Difficulty increased: " + _difficultyLevel);
        }
    }
}
