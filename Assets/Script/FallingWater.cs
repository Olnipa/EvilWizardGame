using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FallingWater : MonoBehaviour
{
    [SerializeField] private WaterDrop _waterdrop;
    [SerializeField] private float _timeBetweenDrops;

    private const string _waterDropped = "WaterDropped";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWaterDropActive()
    {
        _waterdrop.gameObject.SetActive(true);
    }

    private IEnumerator WaitBeforeNextDropJob()
    {
        yield return new WaitForSeconds(_timeBetweenDrops);
        _animator.SetTrigger(_waterDropped);
    }

    public void DropNextDrop()
    {
        StartCoroutine(WaitBeforeNextDropJob());
    }
}