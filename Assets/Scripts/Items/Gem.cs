using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public event Action<Gem> GemCollected;

    public void Collect()
    {
        GemCollected?.Invoke(this);
    }
}