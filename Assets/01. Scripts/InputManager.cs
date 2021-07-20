using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager : InputKeyDictionary
{
    [SerializeField] static KeyCode jumpKey = KeyCode.Space;
    [SerializeField] static KeyCode attackKey = KeyCode.A;

    public KeyCode JumpKey => jumpKey;
    public KeyCode AttackKey => attackKey;

    public InputManager() : base(jumpKey, attackKey) { }

    public Vector2 MoveKeyInput()
    {
        Vector2 playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        return playerInput;
    }

    public bool IsMove() => (MoveKeyInput() != Vector2.zero);
    

}