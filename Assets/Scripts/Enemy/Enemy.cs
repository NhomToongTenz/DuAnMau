using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsPlayer;
        
        [Header("Stunned info")]
        public float stunDuration;

        public Vector2 stunDirection;
        protected bool canBeStunned;
        [SerializeField] protected GameObject counterImage;
        
        [Header("Move Info")]
        public float moveSpeed = 3f;
        public float idleTime = 1f;
        public float battleTime;
        public float defaultMoveSpeed;
        
        [Header("Attack info")]
        public float atkDistance;
        public float atkCooldown;
        
        [HideInInspector] public float lastTimeAttacked;
        public string lastAnimBoolName{get; private set;}
        
        public EnemyStateMachine stateMachine { get; private set; }
    }
}