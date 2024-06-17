using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace DefaultNamespace.Player.InputSystem
{
    public class InputSystemHandler : MonoBehaviour
    {

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();

        }


    }
}