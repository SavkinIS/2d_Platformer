using System;
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
            gem.TriggerEntered += GemHited;
            gem.gameObject.name = $"Gem {index + 1}";
            _gems.Add(gem);
        }
    }

    private void OnEnable()
    {
        foreach (var gem in _gems)
            gem.TriggerEntered += GemHited;
    }

    private void OnDisable()
    {
        foreach (var gem in _gems)
            gem.TriggerEntered -= GemHited;
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


    private void GemHited(Collider2D collider, Gem gem)
    {
        if (collider.TryGetComponent(out GemCollector gemCollector))
        {
            gemCollector.Collect(gem);
            Destroy(gem.gameObject);
        }
    }
}