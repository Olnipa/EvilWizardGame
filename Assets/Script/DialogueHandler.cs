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

    private TextMeshPro _text;

    private const string HelloMessage = "Hello! I see that this wizard turned you into a living chicken leg. Help me please. Eat all those apples on the other side of the lake and then I will help you.";
    private const string MessageDestroyMore = "I feel there are more apples exist. Please, eat all of them.";
    private const string MessageFindKey = "You've eaten all of them. Thank you! Now listen to me carefully: I saw how wizard was hiding something yellow under the dog's plate. I hope it will help you! Good luck!";
    private const string MessageBeforeOpenChest = "You found a key! Now open the chest under the apple tree...";
    private const string MessageToGhost = "Ahaha, I see that you managed to open the chest. Great work. And now you will join the ranks of my guards. Ahahaha.";

    private void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        _text.enabled = false;
        _text.text = HelloMessage;
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
                _text.text = MessageDestroyMore;
                _text.enabled = true;
            }
            else if (mainCharacter.AppleCount == _appleHandler.TotalAppleCount && mainCharacter.IsMainCharacterSprite && mainCharacter.HaveAKey == false)
            {
                _text.text = MessageFindKey;
                _text.enabled = true;
                _platform.gameObject.SetActive(true);
            }
            else if (mainCharacter.HaveAKey && mainCharacter.IsMainCharacterSprite)
            {
                _text.text = MessageBeforeOpenChest;
                _text.enabled = true;
            }
            else if (mainCharacter.IsMainCharacterSprite == false)
            {
                _text.text = MessageToGhost;
                _text.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter mainCharacter))
        {
            _text.enabled = false;

            if (mainCharacter.IsMainCharacterSprite == false)
            {
                _finishGameHandler.StartFinishGameScene();
            }
        }
    }
}