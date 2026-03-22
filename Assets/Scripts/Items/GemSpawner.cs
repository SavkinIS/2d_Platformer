using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _gemSpawnPoint;
    [SerializeField] private Gem _gemPrefab;

    private List<Gem> _gems = new List<Gem>();

    private void Start()
    {
        for (var index = 0; index < _gemSpawnPoint.Count; index++)
        {
            var spawnPoint = _gemSpawnPoint[index];
            var gem = Instantiate(_gemPrefab, transform);
            gem.transform.position = spawnPoint.position;
            gem.gameObject.name = $"Gem {index + 1}";
            gem.Collected += Collect;
            _gems.Add(gem);
        }
    }

    private void OnEnable()
    {
        if (_gems.Count > 0)
        {
            foreach (var gem in _gems)
                gem.Collected += Collect;
        }
    }


    private void OnDisable()
    {
        if (_gems.Count > 0)
        {
            foreach (var gem in _gems)
                gem.Collected -= Collect;
        }
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        string pointNameTemplete = "Point ";
        int pointNumber = 1;

        if (Application.isEditor)
        {
            foreach (Transform child in _gemSpawnPoint)
            {
                child.name = $"{pointNameTemplete}{pointNumber}";
                pointNumber++;
            }
        }
    }

#endif

    private void Destroy(Gem gem)
    {
        Destroy(gem.gameObject);
    }

    private void Collect(CollectionItem collectionItem)
    {
        if (collectionItem is Gem gem)
        {
            gem.Collected -= Collect;
            _gems.Remove(gem);
            Destroy(gem);
        }
    }
}