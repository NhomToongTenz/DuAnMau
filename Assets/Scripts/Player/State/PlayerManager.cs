using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;
    public int currency;
    
    private void Awake()
    {   
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    
    public bool HaveEnoughMoney(int amount)
    {
        if(amount > currency)
            return false;
        
        currency -= amount;
        
        return true;
        
    }

    public int GetCurrency() => currency;
}
