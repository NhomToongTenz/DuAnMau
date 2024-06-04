using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkelontonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skelonton enemySkelonton => GetComponentInParent<Enemy_Skelonton>();
    
    private void AnimationTriggers()
    {
        enemySkelonton.AnimationFinishTrigger();
    }
    
    private void AttackTriggers()
    {
        
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(enemySkelonton.attackCheck.position, enemySkelonton.attackCheckRadius);

        foreach (var hit in collider2Ds)
        {
            if (hit.GetComponent<Player>() != null)
            { 
                PlayerStats _target = hit.GetComponent<PlayerStats>();
                enemySkelonton.stats.DoDamge(_target);
               
            }
        }
    }
    
    protected void OpenCounterWindow() => enemySkelonton.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => enemySkelonton.CloseCounterAttackWindow();
    
}
