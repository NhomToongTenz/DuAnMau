using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
   public Vector2 movementInput {get; private set;}
    public bool jumpInput {get; private set;}
   
   public void OnMoveInput(InputAction.CallbackContext context)
   {
       movementInput = context.ReadValue<Vector2>();
   }
   
   public void UseJumpInput() => jumpInput = false;
   
   public void OnJumpInput(InputAction.CallbackContext context)
   {
       if(context.started)
       {
          jumpInput = true;
       }
   }
  
    
}
