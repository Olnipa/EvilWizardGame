using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private MainCharacter _character;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _apple;
    [SerializeField] private AppleHandler _appleHandler;

    private void Start()
    {
        UpdateUIInformation();
    }

    public void UpdateUIInformation()
    {
        _health.text = Convert.ToString(_character.Health);
        _apple.text = Convert.ToString(_character.AppleCount) + " / " + Convert.ToString(_appleHandler.TotalAppleCount);
    }
}