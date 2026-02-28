using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavesSettings", menuName = "CharacterSettings/WaveSettings")]
public class WavesScriptableObject : ScriptableObject
{
    [Header("Wave config")]
    [SerializeField] public List<WaveData> Waves;
}