using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioGroup> _audioGroups;

    private Dictionary<AudioType, AudioGroup> _groups;
    private Dictionary<AudioType, float> _cooldowns;

    private void Awake()
    {
        _groups = new Dictionary<AudioType, AudioGroup>();
        _cooldowns = new Dictionary<AudioType, float>();

        foreach (var group in _audioGroups)
            _groups[group.Type] = group;
    }

    public void Play(AudioType type)
    {
        if (_groups.TryGetValue(type, out AudioGroup group) == false)
            return;

        if (group.Cooldown > 0)
        {
            if (_cooldowns.TryGetValue(type, out float lastTime) && Time.time < lastTime + group.Cooldown)
                return;

            _cooldowns[type] = Time.time;
        }

        AudioClip clip = group.GetRandomClip();

        if (clip == null)
            return;

        _audioSource.pitch = group.GetRandomPitch();
        _audioSource.PlayOneShot(clip, group.Volume);
    }
}