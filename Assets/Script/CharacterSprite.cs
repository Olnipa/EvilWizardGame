using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class CharacterSprite : MonoBehaviour
{
    [SerializeField] private MainCharacter _character;

    private SpriteRenderer _spriteRenderer;

    private const string IsMovingAnimation = "IsMoving";
    private const string AnimationHitName = "Hit";
    
    public Animator Animator { get; private set; }
    public bool AnimationHitIsPlay { get; private set; }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_character.IsAlive)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                RotateSprite(false);
    
                if (_character.IsTransformed)
                    SetIsMovingAnimation(true);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                RotateSprite(true);

                if (_character.IsTransformed)
                    SetIsMovingAnimation(true);
            }
            else if (_character.IsTransformed)
            {
                    SetIsMovingAnimation(false);
            }
        }
    }

    private void SetIsMovingAnimation(bool isMoving)
    {
        Animator.SetBool(IsMovingAnimation, isMoving);
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
            Animator.SetTrigger(AnimationHitName);
            AnimationHitIsPlay = true;
        }
    }
}