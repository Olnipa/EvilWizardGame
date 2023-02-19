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
                IsOpen = true;
                _animator.SetTrigger(OpenChestTrigger);
                _characterSprite.Animator.runtimeAnimatorController = _newCharacterAnimatorController;
                _character.HumanoidMover.enabled = false;
                _character.TransformInToGhost();
                _character.AddComponent<GhostMover>();
                _audioSource.Play();
                _backgroundMusicHandler.PlayGhostMusic();
            }
        }
    }
}