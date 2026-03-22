using System.Collections.Generic;
using UnityEngine;

public class AidKitHolder : MonoBehaviour
{
    [SerializeField] private List<AidKit> _aidKits;

    private void OnEnable()
    {
        if (_aidKits.Count > 0)
        {
            foreach (var aidKit in _aidKits)
                aidKit.Collected += Collected;
        }
    }

    private void OnDisable()
    {
        if (_aidKits.Count > 0)
        {
            foreach (var aidKit in _aidKits)
                aidKit.Collected -= Collected;
        }
    }

    private void Destroy(AidKit aidKit)
    {
        Destroy(aidKit.gameObject);
    }

    private void Collected(CollectionItem collectionItem)
    {
        if (collectionItem is AidKit aidKit)
        {
            aidKit.Collected -= Collected;
            _aidKits.Remove(aidKit);
            Destroy(aidKit);
        }
    }
}