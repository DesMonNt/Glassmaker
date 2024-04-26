using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;

public class RandomSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabLetter")] [SerializeField] private GameObject _prefabLetter1;
    [SerializeField] private GameObject _prefabLetter2;
    [SerializeField] private GameObject _prefabLetter3;
    private int _c;
    private int _r;

    private GameObject[] _letters = new GameObject [3];

    private readonly Vector3[] _pointsToSpawn = {
        new(-500, 500),
        new(0, 500),
        new(500, 500),
        new(1200, 0),
        new(500, -500),
        new(0, -500),
        new(-500, -500),
        new(0, -1200)
    };

    void Start() => _letters = new[] { _prefabLetter1, _prefabLetter2, _prefabLetter3 };

    private void Spawn()
    {
        var random = new Random();
        var rndCoordinateIndex = random.Next(0, _pointsToSpawn.Length);
        var rndPos = _pointsToSpawn[rndCoordinateIndex];
        var rndPrefabIndex = random.Next(0, _letters.Length);
        var rndPrefab = _letters[rndPrefabIndex];

        Instantiate(rndPrefab, rndPos, Quaternion.identity);
        //StartCoroutine(DelaySpawn());
    }

    void Update()
    {
        _c++;
        if (_c != 120)
            return;
        Spawn();
        _c = 0;

        //StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("FightingScene");
    }

    IEnumerator Destroyer(Collider2D other)
    {
        AccuracyText.maxSum+=3;
        yield return new WaitForSeconds (0);
        Destroy(other.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("C") || other.gameObject.CompareTag("Z") || other.gameObject.CompareTag("X")) 
            StartCoroutine(Destroyer(other));
    }
}
