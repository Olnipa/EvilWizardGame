using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private AudioClip _keySoundPickup;
    [SerializeField] private UnityEvent _keyPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MainCharacter mainCharacter))
        {
            AudioSource.PlayClipAtPoint(_keySoundPickup, transform.position);
            _keyPickedUp.Invoke();
        }
    }
}
