
using UnityEngine;

public class ThunderStikeController : MonoBehaviour
{
   protected  virtual void OnTriggerEnter2D(Collider2D other)
   {
      if (other.GetComponent<Enemy>() != null) 
      {
         PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
         
         EnemyStats enemyTarget = other.GetComponent<EnemyStats>();
         
         playerStats.DoMagicDamage(enemyTarget);
      }
   }
}
