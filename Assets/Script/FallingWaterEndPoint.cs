using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class FallingWaterEndPoint : MonoBehaviour
{
    private Animator _animator;
    private float _startPositionX;
    
    private const string AnimatorTargetIsReached = "TargetIsReached";

    private void Awake()
    {
        _startPositionX = GetComponentInParent<FallingWater>().transform.position.x;
        transform.position = new Vector3(_startPositionX, transform.position.y, transform.position.z);
        _animator = GetComponent<Animator>();
    }

    public void SetTriggerAnimatorTargetIsReached()
    {
        _animator.SetTrigger(AnimatorTargetIsReached);
    }
}