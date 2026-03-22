using UnityEngine;

public class AidKit : CollectionItem
{
    [SerializeField] private float _healthRestore = 50f;
    
    public float HealthRestore => _healthRestore;
}