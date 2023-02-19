using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FallingWater : MonoBehaviour
{
    [SerializeField] private WaterDrop _waterdrop;
    [SerializeField] private float _timeBetweenDrops;

    private Animator _animator;
    private WaitForSeconds _secondsBetweenDrop;

    private const string WaterDropped = "WaterDropped";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _secondsBetweenDrop = new WaitForSeconds(_timeBetweenDrops);
    }

    private IEnumerator WaitBeforeNextDropJob()
    {
        yield return _secondsBetweenDrop;
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