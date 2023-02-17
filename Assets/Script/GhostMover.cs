using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]

public class GhostMover : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _contactFilter;

    private Rigidbody2D _rigidBody;
    private float _speed = 5;

    private float _translationX;
    private float _translationY;
    private RaycastHit2D[] _raycastHitsX = new RaycastHit2D[1];
    private RaycastHit2D[] _raycastHitsY = new RaycastHit2D[1];
    private int collisionXCount;
    private int collisionYCount;

    private float _maxDistanceToBlock = 0.05f;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            _translationX = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
            _translationY = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

            collisionXCount = _rigidBody.Cast(new Vector2(_translationX, 0), _contactFilter, _raycastHitsX, _maxDistanceToBlock);
            collisionYCount = _rigidBody.Cast(new Vector2(0, _translationY), _contactFilter, _raycastHitsY, _maxDistanceToBlock);

            if (collisionXCount == 0)
                transform.Translate(_translationX, 0, 0);
            else
                _rigidBody.velocity = Vector2.zero;

            if (collisionYCount == 0)
                transform.Translate(0, _translationY, 0);
            else
                _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            transform.Translate(0, 0, 0);
            _rigidBody.velocity = Vector2.zero;
        }
    }
}