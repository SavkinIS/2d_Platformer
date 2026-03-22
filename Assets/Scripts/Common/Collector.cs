using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<Gem> GemColleted;
    public event Action<AidKit> AidKitColleted;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gem gem))
        {
            Collect(gem);
        }
        else if (collision.TryGetComponent(out AidKit aidKit))
        {
            Collect(aidKit);
        }
    }
    
    private void Collect(Gem gem)
    {
        GemColleted?.Invoke(gem);
        gem.Collect();
    }
    
    private void Collect(AidKit aidKit)
    {
        AidKitColleted?.Invoke(aidKit);
        aidKit.Collect();
    }
}