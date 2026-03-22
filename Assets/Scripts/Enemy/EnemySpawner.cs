using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Path _path;

    private void Awake()
    {
        var enemy = Instantiate(_enemyPrefab, transform);
        
        enemy.transform.position = _spawnPoint.position;
        enemy.Initialize(_path);

        enemy.CalledDead += CallEnemyDead;
    }

    private void CallEnemyDead(GameObject obj)
    {
        obj.SetActive(false);
        Destroy(obj);
    }
}