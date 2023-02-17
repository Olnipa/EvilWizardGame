using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishGameHandler : MonoBehaviour
{
    [SerializeField] private MainCharacter _character;
    [SerializeField] private Transform _targetToMove;
    [SerializeField] private Transform _targetToPatrol;
    [SerializeField] private FinalScreen _finalScreen;
    [SerializeField] private AudioSource _finalSound;
    [SerializeField] private BackgroundMusicHandler _backgroundMusicHandler;

    private float _secondsToWait = 5;

    private void MoveCharacterToChest()
    {
        _character.transform.position = _targetToMove.position;
    }

    private void TryDisableOldControl()
    {
        if (_character.TryGetComponent(out GhostMover _mover))
        {
            _mover.enabled = false;
        }
    }

    private void AddNewMover()
    {
        EnemyMover newMover = _character.AddComponent<EnemyMover>();
        newMover._endPointTransform = _targetToPatrol;
        _character.GetComponentInChildren<CharacterSprite>().AddComponent<SpriteSpinner>();
    }

    private IEnumerator ShowTheEndBackgroundJob()
    {
        yield return new WaitForSeconds(_secondsToWait);
        _finalScreen.gameObject.SetActive(true);
        _backgroundMusicHandler.PlayGhostMusic();
        _finalSound.Play();
    }

    public void StartFinishGameScene()
    {
        MoveCharacterToChest();
        TryDisableOldControl();
        AddNewMover();
        StartCoroutine(ShowTheEndBackgroundJob());
    }
}
