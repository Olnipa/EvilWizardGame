using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleHandler : MonoBehaviour
{
    private Apple[] _coinApples;
    public int TotalAppleCount { get; private set; }

    private void Start()
    {
        _coinApples = GetComponentsInChildren<Apple>();
        TotalAppleCount = _coinApples.Length;
    }
}
