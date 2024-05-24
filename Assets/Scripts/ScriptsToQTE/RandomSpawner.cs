using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class RandomSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_prefabLetter1")] [SerializeField] private GameObject prefabLetter1;
    [FormerlySerializedAs("_prefabLetter2")] [SerializeField] private GameObject prefabLetter2;
    [FormerlySerializedAs("_prefabLetter3")] [SerializeField] private GameObject prefabLetter3;
    public static int SpawnDelay;
    private int _r;
    public static bool CanSpawn;

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

    public SpriteRenderer square;

    private void Start()
    {
        CanSpawn = true;
        SpawnDelay = 86;
        _letters = new[] { prefabLetter1, prefabLetter2, prefabLetter3 };
    }

    private void Spawn()
    {
        var random = new Random();
        var rndCoordinateIndex = random.Next(0, _pointsToSpawn.Length);
        var randomPosition = _pointsToSpawn[rndCoordinateIndex];
        var rndPrefabIndex = random.Next(0, _letters.Length);
        var rndPrefab = _letters[rndPrefabIndex];

        Instantiate(rndPrefab, randomPosition, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (SpawnDelay % 20 != 0)
        {
            SpawnDelay++;
            if (SpawnDelay % 240 == 0) StartCoroutine(EndGame());
            return;
        }

        SpawnDelay++;
        Spawn();
    }

    private IEnumerator EndGame()
    {
        CanSpawn = false;
        Fight.IsEndQte = true;
        AccuracyText.IsEnd = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private static IEnumerator Destroyer(Collider2D other)
    {
        AccuracyText.MaxSum += 3;
        yield return new WaitForSeconds(2);
        Destroy(other.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("C") || other.gameObject.CompareTag("Z") || other.gameObject.CompareTag("X")) 
            StartCoroutine(Destroyer(other));
    }
}
