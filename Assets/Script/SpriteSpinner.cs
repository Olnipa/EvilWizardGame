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

    public void RotateSprite()
    {
        if (_spriteRenderer.flipX == true)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
    }
}