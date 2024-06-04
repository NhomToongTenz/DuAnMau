using UnityEngine;

[CreateAssetMenu(fileName = "IceAndFireEffect", menuName = "Inventory System/Items/Effects/IceAndFireEffect")]
public class IceAndFireEffect : ItemEffect
{
    [SerializeField] private GameObject iceAndFirePrefab;
    [SerializeField] private  float xVelocity;
    public override void ExecuteEffect(Transform _respawnPos)
    {
        Player player = PlayerManager.instance.player;
        
        bool thirdAttack = player.GetComponent<Player>().PrimaryAttackStateState.comboAttackCounter == 2;

        if (thirdAttack)
        {
            GameObject _iceAndFire = Instantiate(iceAndFirePrefab, _respawnPos.position, player.transform.rotation);
        
            _iceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDirection, 0);
        }
        
        
    }
}
