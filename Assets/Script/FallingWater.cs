using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FallingWater : MonoBehaviour
{
    [SerializeField] private WaterDrop _waterdrop;
    [SerializeField] private float _timeBetweenDrops;

    private Animator _animator;

    private const string WaterDropped = "WaterDropped";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator WaitBeforeNextDropJob()
    {
        yield return new WaitForSeconds(_timeBetweenDrops);
        _animator.SetTrigger(WaterDropped);
    }

    public void SetWaterDropActive()
    {
        _waterdrop.gameObject.SetActive(true);
    }

    public void DropNextDrop()
    {
        StartCoroutine(WaitBeforeNextDropJob());
    }
}