using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Attack details")]
        public Vector2[] attackMovement;
        public float counterAttackDuration = 0.5f;

        public bool isBusy { get; private set; }

        [Header("Move info")]
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float arrowsRetuernImpact;
        public float defaultMoveSpeed;
        public float defaultJumpForce;

        [Header("Dash info")]
        public float dashSpeed = 10f;
        public float dashDuration = 0.2f;
        private float defaultDashSpeed;
        public float dashDir { get; private set; }

        //public SkillManager skillManager{ get; privet set; }
        //pulic GameObject arrowPrefab{ get; privet set; }
        //public PlayerFx fx{ get; privet set; }

    }
}