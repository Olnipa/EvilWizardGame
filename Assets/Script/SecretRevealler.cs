using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent (typeof(TilemapRenderer))]

public class SecretRevealler : MonoBehaviour
{
    [SerializeField] private bool _hideAfterExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryToDisableTilemap(collision, false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_hideAfterExit)
        {
            TryToDisableTilemap(collision, true);
        }
    }

    private void TryToDisableTilemap(Collider2D collision, bool isEnable)
    {
        if (collision.TryGetComponent(out MainCharacter character))
        {
            GetComponent<TilemapRenderer>().enabled = isEnable;
        }
    }
}