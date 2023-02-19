using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private AppleHandler _appleHandler;
    [SerializeField] private FinishGameHandler _finishGameHandler;
    /*[SerializeField] private ChestOfEvil _chest;*/
    [SerializeField] private Platform _platform;
    [SerializeField] private string[] _dialogueTexts;

    private TextMeshPro _text;

    private const int HelloMessageIndex = 0;
    private const int DestroyMoreMessageIndex = 1;
    private const int FindKeyMessageIndex = 2;
    private const int FindChestMessageIndex = 3;
    private const int FinalMessageIndex = 4;

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _text.enabled = false;
        _text.text = _dialogueTexts[HelloMessageIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter mainCharacter))
        {
            if (mainCharacter.AppleCount == 0)
            {
                _text.enabled = true;
            }
            else if (mainCharacter.AppleCount > 0 && mainCharacter.AppleCount < _appleHandler.TotalAppleCount)
            {
                _text.text = _dialogueTexts[DestroyMoreMessageIndex];
                _text.enabled = true;
            }
            else if (mainCharacter.AppleCount == _appleHandler.TotalAppleCount && mainCharacter.IsTransformed && mainCharacter.HaveAKey == false)
            {
                _text.text = _dialogueTexts[FindKeyMessageIndex];
                _text.enabled = true;
                _platform.gameObject.SetActive(true);
            }
            else if (mainCharacter.HaveAKey && mainCharacter.IsTransformed)
            {
                _text.text = _dialogueTexts[FindChestMessageIndex];
                _text.enabled = true;
            }
            else if (mainCharacter.IsTransformed == false)
            {
                _text.text = _dialogueTexts[FinalMessageIndex];
                _text.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter mainCharacter))
        {
            _text.enabled = false;

            if (mainCharacter.IsTransformed == false)
            {
                _finishGameHandler.StartFinishGameScene();
            }
        }
    }
}