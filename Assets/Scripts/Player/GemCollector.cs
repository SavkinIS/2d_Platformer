using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private int _gemCount;

    public void Collect(Gem gem)
    {
        _gemCount++;
    }
}
