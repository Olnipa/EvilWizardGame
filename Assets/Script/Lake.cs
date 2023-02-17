using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MainCharacter mainCharacter))
        {
            _audioSource.Play();
        }
    }
}