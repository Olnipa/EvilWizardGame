using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ChestOfEvil : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _newCharacterAnimatorController;
    [SerializeField] private CharacterSprite _characterSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BackgroundMusicHandler _backgroundMusicHandler;

    private Animator _animator;
    private const string OpenChestTrigger = "Open";

    public bool IsOpen { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter _character))
        {
            if (_character.HaveAKey && IsOpen == false)
            {
                _animator.SetTrigger(OpenChestTrigger);
                Animator characterAnimator = _characterSprite.GetComponent<Animator>();
                characterAnimator.runtimeAnimatorController = _newCharacterAnimatorController;
                HumanoidMover humanoidMover = _character.GetComponent<HumanoidMover>();
                humanoidMover.enabled = false;
                _character.SetBoolIsMainCharacterSpriteFalse();
                _character.AddComponent<GhostMover>();
                IsOpen = true;
                _audioSource.Play();
                _backgroundMusicHandler.PlayGhostMusic();
            }
        }
    }
}
