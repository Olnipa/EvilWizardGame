using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private UIHandler _uiHandler;
    [SerializeField] private CharacterSprite _characterSprite;
    [SerializeField] private float _horisontalHitRepelDistance = 0.07f;
    [SerializeField] private float _verticalHitRepelDistance = 0.03f;
    [SerializeField] private float _timeOfLifeUnderWater = 2f;
    [SerializeField] private UnityAction _isMoved;
    [SerializeField] private UnityEvent _damageTaken;
    [SerializeField] private UnityEvent _deaded;

    private Rigidbody2D _rigidBody;
    private int _defaultHealth = 3;
    private bool _isUnderWater;
    private Coroutine _underWaterCoroutine;

    public bool HaveAKey { get; private set; }
    public int Health { get; private set; }
    public int AppleCount { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsMainCharacterSprite { get; private set; }

    private void Start()
    {
        IsAlive = true;
        Health = _defaultHealth;
        _rigidBody = GetComponent<Rigidbody2D>();
        IsMainCharacterSprite = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (TryTakeDamage(collision) == false)
        {
            TryToDrown(collision);
        }
    }

    private void TryToDrown(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lake lake) && IsMainCharacterSprite)
        {
            _isUnderWater = true;
            _underWaterCoroutine = StartCoroutine(WaitFewSecondsBeforeDrownJob(_timeOfLifeUnderWater));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lake lake))
        {
            StopCoroutine(_underWaterCoroutine);
            _isUnderWater = false;
        }
    }

    private IEnumerator WaitFewSecondsBeforeDrownJob(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        
        if (_isUnderWater)
            IsAlive = false;
    }

    private bool TryTakeDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && _characterSprite.AnimationHitIsPlay == false && IsAlive && IsMainCharacterSprite)
        {
            _damageTaken.Invoke();
            CastAside(collision);
            return true;
        }

        return false;
    }

    public void CastAside(Collider2D collision)
    {
        if (collision.transform.position.x < transform.position.x)
            _rigidBody.AddForce(new Vector2(_horisontalHitRepelDistance, _verticalHitRepelDistance), ForceMode2D.Impulse);
        else
            _rigidBody.AddForce(new Vector2(_horisontalHitRepelDistance * -1, _verticalHitRepelDistance), ForceMode2D.Impulse);
    }

    public void TakeDamage()
    {
        Health--;
        _uiHandler.UpdateUIInformation();
        
        if (Health <= 0)
        {
            IsAlive = false;
            _deaded.Invoke();
        }
    }

    public void CollectApple()
    {
        AppleCount++;
    }

    public void PickUpKey()
    {
        HaveAKey = true;
    }

    public void SetBoolIsMainCharacterSpriteFalse()
    {
        IsMainCharacterSprite = false;
    }
}