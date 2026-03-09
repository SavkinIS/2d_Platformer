using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<Gem> GemColleted;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gem gem))
        {
            Collect(gem);
        }
    }
    
    private void Collect(Gem gem)
    {
        GemColleted?.Invoke(gem);
    }
}