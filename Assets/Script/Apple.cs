using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    [SerializeField] private AudioClip _appleCollectionSound;
    [SerializeField] private UIHandler _uiHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MainCharacter mainCharacter))
        {
            AudioSource.PlayClipAtPoint(_appleCollectionSound, transform.position);
            mainCharacter.CollectApple();
            _uiHandler.UpdateUIInformation();
            gameObject.SetActive(false);
        }
    }
}