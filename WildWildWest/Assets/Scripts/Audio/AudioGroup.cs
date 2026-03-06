using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioGroup
{
    public AudioType Type;
    public List<AudioClip> Clips;

    [Range(0f, 1f)] public float Volume;
    [Range(0.1f, 1f)] public float MinPitch;
    [Range(0.1f, 1f)] public float MaxPitch;
    public float Cooldown;

    public AudioClip GetRandomClip()
    {
        if(Clips == null || Clips.Count == 0)
            return null;

        return Clips[Random.Range(0, Clips.Count)];
    }

    public float GetRandomPitch()
    {
        return Random.Range(MinPitch, MaxPitch);
    }
}