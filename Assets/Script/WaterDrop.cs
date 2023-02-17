using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] private float _fallingSpeed = 2;
    [SerializeField] private Transform _target;
    [SerializeField] private UnityEvent _waterEndPointReached;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.position = _startPosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _fallingSpeed * Time.deltaTime);

        if (transform.position == _target.position)
        {
            _waterEndPointReached.Invoke();
        }
    }
}