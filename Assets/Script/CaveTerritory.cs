using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTerritory : MonoBehaviour
{
    [SerializeField] private BackgroundMusicHandler _musicHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter character) && character.IsMainCharacterSprite)
        {
            _musicHandler.PlayCaveMusic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter character) && character.IsMainCharacterSprite)
        {
            _musicHandler.PlayMainMusic();
        }
    }
}
