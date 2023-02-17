using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]

public class HumanoidMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private AudioSource _jumpSound;

    private LayerMask _layerMask;
    private Vector2 _velocity;
    private float _minGroundNormalY = .65f;

    private Vector2 _groundNormal;
    private Rigidbody2D _rigidBody;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private Vector2 _targetVelocity;

    private const float ConstantGravityModifier = 2f;
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;
    private const string HorizontalAxisName = "Horizontal";
    private const string DefalustLayerName = "Default";
    
    public bool _grounded { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidBody.gravityScale = 0;
    }

    private void OnDisable()
    {
        _rigidBody.gravityScale = ConstantGravityModifier;
    }

    private void Start()
    {
        _gravityModifier = ConstantGravityModifier;
        _contactFilter.useTriggers = false;
        _layerMask = LayerMask.GetMask(DefalustLayerName);
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis(HorizontalAxisName), 0) * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _velocity.y = _jumpForce;
            _jumpSound.Play();
        }
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 moveDirection = moveAlongGround * deltaPosition.x;

        ToMove(moveDirection, false);

        moveDirection = Vector2.up * deltaPosition.y;

        ToMove(moveDirection, true);
    }

    private void ToMove(Vector2 moveDirection, bool IsYMovement)
    {
        float distance = moveDirection.magnitude;

        if (distance > MinMoveDistance)
        {
            int collisionCount = _rigidBody.Cast(moveDirection, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < collisionCount; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _grounded = true;

                    if (IsYMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody.position = _rigidBody.position + moveDirection.normalized * distance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder ladder))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _velocity.y = 0;
                _gravityModifier = 0;
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                _gravityModifier = ConstantGravityModifier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder ladder))
        {
            _gravityModifier = ConstantGravityModifier;
        }
    }
}