using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _mainThemeAudio;
    [SerializeField] private AudioSource _caveThemeAudio;
    [SerializeField] private AudioSource _ghostAudio;

    [SerializeField] private float _timeToChangeVolume = 1f;
    [SerializeField] private float _unitOfVolumeChange = 0.1f;

    private float _minVolume = 0;
    private float _maxVolume = 0.5f;
    private Coroutine _coroutine;

    public AudioSource ActualAudio { get; private set; }

    public void PlayGhostMusic()
    {
        PlayNextSound(_ghostAudio);
    }

    public void PlayCaveMusic()
    {
        PlayNextSound(_caveThemeAudio);
    }

    public void PlayMainMusic()
    {
        PlayNextSound(_mainThemeAudio);
    }

    private void Start()
    {
        ActualAudio = _mainThemeAudio;
        ActualAudio.volume = _maxVolume;
        ActualAudio.Play();
        ActualAudio.loop = true;
        _caveThemeAudio.Play();
        _caveThemeAudio.Pause();
        _ghostAudio.Play();
        _ghostAudio.Pause();    
    }

    private IEnumerator ChangeVolumeJob(bool toVolumeDown)
    {
        float targetVolume;
        float timeToWait;

        timeToWait = _timeToChangeVolume / (_maxVolume / _unitOfVolumeChange);

        if (toVolumeDown)
            targetVolume = _minVolume;
        else
            targetVolume = _maxVolume;

        while (ActualAudio.volume != targetVolume)
        {
            Debug.Log("Volume: " + ActualAudio.volume);
            ActualAudio.volume = Mathf.MoveTowards(ActualAudio.volume, targetVolume, _unitOfVolumeChange);

            yield return new WaitForSecondsRealtime(timeToWait);
        }
    }

    private IEnumerator ChangeAudioSourceJob(AudioSource nextAudioSource)
    {
        yield return StartCoroutine(ChangeVolumeJob(true));

        ActualAudio.Pause();
        ActualAudio = nextAudioSource;
        ActualAudio.UnPause();

        yield return StartCoroutine(ChangeVolumeJob(false));

        _coroutine = null;
    }

    private void PlayNextSound(AudioSource nextAudioSource)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeAudioSourceJob(nextAudioSource));
    }
}