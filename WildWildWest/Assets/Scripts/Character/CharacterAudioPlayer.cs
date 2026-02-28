using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private List<AudioClip> _stepsClips;
    private AudioClip _damageClip;
    private AudioClip _deadClip;

    private float _delayBetweenStep;
    private float _minPitchSteps;
    private float _maxPitchSteps;

    private float _lastStepTime;

    public void Initialize(AudioSource audioSource, List<AudioClip> stepsClips, AudioClip damageClip, AudioClip deadClip, float delayBetweenStep, float minPitchSteps, float maxPitchSteps)
    {
        _audioSource = audioSource;
        _stepsClips = stepsClips;
        _damageClip = damageClip;
        _deadClip = deadClip;
        _delayBetweenStep = delayBetweenStep;
        _minPitchSteps = minPitchSteps;
        _maxPitchSteps = maxPitchSteps;
    }

    public void PlayStepsClip(bool canPlaySteps)
    {
        if (canPlaySteps && Time.time >= _lastStepTime + _delayBetweenStep)
        {
            int randomClipIndex = Random.Range(0, _stepsClips.Count);

            SetRandomPitch();

            _audioSource.PlayOneShot(_stepsClips[randomClipIndex]);
            _lastStepTime = Time.time;
        }
    }

    public void PlayDamageClip()
    {
        SetRandomPitch();

        _audioSource.PlayOneShot(_damageClip);
    }

    public void PlayDeadClip()
    {
        _audioSource.PlayOneShot(_deadClip);
    }

    private void SetRandomPitch()
    {
        float randomPitch = Random.Range(_minPitchSteps, _maxPitchSteps);
        _audioSource.pitch = randomPitch;
    }
}