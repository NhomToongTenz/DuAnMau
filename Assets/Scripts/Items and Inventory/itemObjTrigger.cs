using System;
using UnityEngine;

public class itemObjTrigger : MonoBehaviour
{
    private itemObj myItemObj => GetComponentInParent<itemObj>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if(other.GetComponent<CharacterStats>().isDead)
                return;
                
            
            Debug.Log("Player picked up item!");
            myItemObj.PickupItem();
            
        }
    }
}
