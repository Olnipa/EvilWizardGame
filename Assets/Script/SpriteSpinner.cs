using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteSpinner : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private EnemyMover _enemyMover;

    private void Start()
    {
        _enemyMover = GetComponentInParent<EnemyMover>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_enemyMover.IsMovingBack)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }
}