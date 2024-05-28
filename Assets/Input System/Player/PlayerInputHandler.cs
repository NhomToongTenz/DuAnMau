using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{   
    public PlayerInputHandler instance {get; private set;}
    
   public Vector2 movementInput {get; private set;}
    public bool jumpInput {get; private set;}
    public bool dashInput {get; private set;}
   
    // send message to the player
    
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
        
    }
    
    void OnDash(InputValue value)
    {
        if(value.isPressed)
        {
            dashInput = true;
        }
        else
        {
            dashInput = false;
        }
    }
   
    
}
