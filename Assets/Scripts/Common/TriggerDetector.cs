using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour 
{
    public Action<Collider2D> TriggerEntered;
    public Action<Collider2D> TriggerStayed;
    public Action<Collider2D> TriggerExited;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEntered?.Invoke(other);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        TriggerStayed?.Invoke(other);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        TriggerExited?.Invoke(other);
    }
}