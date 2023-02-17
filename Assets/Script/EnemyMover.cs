using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] public Transform _endPointTransform;

    private Vector3 _initialPosition;
    private Vector3 _nextTargetPosition;

    public bool IsMovingBack { get; private set; }

    private void Start()
    {
        _initialPosition = transform.position;
        _nextTargetPosition = _endPointTransform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_nextTargetPosition.x, transform.position.y, transform.position.z), _speed * Time.deltaTime);

        if (transform.position.x == _endPointTransform.position.x)
        {
            _nextTargetPosition = _initialPosition;
        }
        else if (transform.position.x == _initialPosition.x)
        {
            _nextTargetPosition = _endPointTransform.position;
        }

        if (_nextTargetPosition.x < transform.position.x)
            IsMovingBack = true;
        else
            IsMovingBack = false;
    }
}