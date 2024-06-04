using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    public float coolDown;
    protected float cooldownTimer;

    protected Player player;
    
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    
    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }
    
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            //useSkill
            UseSkill();
            cooldownTimer = coolDown;
            return true;
        }
        Debug.Log("Skill is on coolDown");
        return false;
    }
    
    public virtual void UseSkill()
    {
        // do some skill specific things
    }
    
    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(_checkTransform.position, 25);
        
        float minDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var collider in collider2Ds)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(_checkTransform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy;
    }
}
