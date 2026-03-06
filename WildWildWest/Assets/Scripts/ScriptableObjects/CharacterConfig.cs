using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = Constants.Configs.CharacterMenuName + nameof(CharacterConfig))]
public class CharacterConfig : ScriptableObject
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
}