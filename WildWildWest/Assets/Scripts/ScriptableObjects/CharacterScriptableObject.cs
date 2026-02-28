using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSettings", menuName = "CharacterSettings/CharacterSettings")]
public class CharacterScriptableObject : ScriptableObject
{
    [Header("Health")]
    public float Health;

    [Header("Gravity")]
    public float GravityForce;
    public float MinVerticalVelocity;

    [Header("Movement")]
    public float MoveSpeed;
    public float RotateSpeed;
    public float FirearmsAngleOffset;
    public float MeleAngleOffset;

    [Header("Audio")]
    public List<AudioClip> StepsClips;
    public float DelayBetweenSteps;
    public float MinPitchSteps;
    public float MaxPitchSteps;
    public AudioClip TakeDamageClip;
    public AudioClip DeadClip;
}