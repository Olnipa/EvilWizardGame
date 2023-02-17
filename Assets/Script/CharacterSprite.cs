using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class CharacterSprite : MonoBehaviour
{
    [SerializeField] private MainCharacter _character;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private const string IsMovingAnimation = "IsMoving";
    private const string AnimationHitName = "Hit";
    
    public bool AnimationHitIsPlay { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_character.IsAlive)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                RotateSprite(false);
    
                if (_character.IsMainCharacterSprite)
                    SetIsMovingAnimation(true);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                RotateSprite(true);

                if (_character.IsMainCharacterSprite)
                    SetIsMovingAnimation(true);
            }
            else if (_character.IsMainCharacterSprite)
            {
                    SetIsMovingAnimation(false);
            }
        }
    }

    private void SetIsMovingAnimation(bool isMoving)
    {
        _animator.SetBool(IsMovingAnimation, isMoving);
    }

    private void RotateSprite(bool isTurnLeft)
    {
        _spriteRenderer.flipX = isTurnLeft;
    }

    public void SetAnimationHitPlayFalse()
    {
        AnimationHitIsPlay = false;
    }

    public void TryPlayHitAnimation()
    {
        if (AnimationHitIsPlay == false)
        {
            _animator.SetTrigger(AnimationHitName);
            AnimationHitIsPlay = true;
        }
    }
}