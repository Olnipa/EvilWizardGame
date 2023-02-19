using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] public Transform _endPointTransform;
    [SerializeField] private SpriteSpinner _spriteSpinner;

    private Vector3 _initialPosition;
    private Vector3 _nextTargetPosition;

    private void Start()
    {
        _initialPosition = transform.position;
        _nextTargetPosition = _endPointTransform.position;

        if (_nextTargetPosition.x < transform.position.x)
            _spriteSpinner.RotateSprite();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_nextTargetPosition.x, transform.position.y, transform.position.z), _speed * Time.deltaTime);

        if (transform.position.x == _endPointTransform.position.x)
        {
            _nextTargetPosition = _initialPosition;
            _spriteSpinner.RotateSprite();
        }
        else if (transform.position.x == _initialPosition.x)
        {
            _nextTargetPosition = _endPointTransform.position;
            _spriteSpinner.RotateSprite();
        }
    }
}