using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D _mainPlatformCollider;

    private void Start()
    {
        _mainPlatformCollider = GetComponentInParent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter character))
        {
            if (character.transform.position.y > transform.position.y)
            {
                _mainPlatformCollider.isTrigger = false;
            }
            else
            {
                _mainPlatformCollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter character))
        {
            if (character.transform.position.y > transform.position.y)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    _mainPlatformCollider.isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter character))
        {
            if (character.transform.position.y > transform.position.y)
            {
                _mainPlatformCollider.isTrigger = false;
            }
            else
            {
                _mainPlatformCollider.isTrigger = true;
            }
        }
    }
}
