using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelBooster : MonoBehaviour
{
    [SerializeField] private float _maxLevelOfWater;
    [SerializeField] private float _amountOfWaterLevelChange;

    public void IncreaseWaterLevel()
    {
        if (transform.position.y < _maxLevelOfWater)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _amountOfWaterLevelChange, transform.position.z);
        }
    }
}