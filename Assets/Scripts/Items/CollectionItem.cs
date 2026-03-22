using System;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    public event Action<CollectionItem> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}