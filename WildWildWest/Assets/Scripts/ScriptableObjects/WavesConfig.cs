using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(WavesConfig), menuName = Constants.Configs.WavesMenuName + nameof(WavesConfig))]
public class WavesConfig : ScriptableObject
{
    [Header("Wave config")]
    [SerializeField] public List<WaveData> Waves;
}