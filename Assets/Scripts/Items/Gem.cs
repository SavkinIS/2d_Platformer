using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public event Action<Collider2D, Gem> TriggerEntered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEntered?.Invoke(other, this);
    }

}