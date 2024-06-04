using SkillManager;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
   private Player player => GetComponentInParent<Player>();
   
   private void AnimationTrigger()
   {
       player.AnimationTrigger();
   }
   
   private void AttackTriggers()
   {
       Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

       foreach (var hit in collider2Ds)
       {
           if (hit.GetComponent<Enemy>() != null)
           {
               EnemyStats _target = hit.GetComponent<EnemyStats>();
               
               if(_target != null)
                   player.stats.DoDamge(_target);
               
               ItemData_Equipment weaponData = Inventory.instance.GetEquipmentType(EquipmentType.Weapon);

               if (weaponData != null)
               {
                   weaponData.Effect(_target.transform);
               }
           }
       }
       
   }
   
   private void ThrowSword()
   {
       SkillManager.SkillManager.instance.swordSkill.CreateSword();
   }
   
   
}
