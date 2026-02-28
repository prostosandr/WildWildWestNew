using UnityEngine;

public class AimTargetMover : MonoBehaviour
{
    [SerializeField] private PlayerInputProvider _input;

    private void Update()
    {
        transform.position = _input.CursorPosition;
    }
}