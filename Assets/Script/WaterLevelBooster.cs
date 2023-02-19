using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelBooster : MonoBehaviour
{
    [SerializeField] private float _maxLevelOfWater = 1.95f;
    [SerializeField] private float _amountOfWaterLevelChange = 0.006f;

    public void IncreaseWaterLevel()
    {
        if (transform.localPosition.y < _maxLevelOfWater)
            transform.position = new Vector3(transform.position.x, transform.position.y + _amountOfWaterLevelChange, transform.position.z);
    }
}