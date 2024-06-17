using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}




public class Sword_Skill : Skill
{   
    public SwordType swordType = SwordType.Regular;
    
    [Header("Bounce info")]
    [SerializeField] private UI_SkillTreeSLot bounceUnlockBtn;
    public bool bounceUnlocked{get; private set;}
    [SerializeField] private int bounceCount;
    [SerializeField] private float bounceForce;
    [SerializeField] private float bounceSpeed;
    
    [Header("Pierce info")]
    [SerializeField] private UI_SkillTreeSLot pierceUnlockBtn;
    public bool pierceUnlocked{get; private set;}
    [SerializeField] private float pierceForce;
    [SerializeField] private int pierceCount;
    
    [Header("Spin info")]
    [SerializeField] private UI_SkillTreeSLot spinUnlockBtn;
    public bool spinUnlocked{get; private set;}
    [SerializeField] private float hitCooldown = 0.35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinForce = 10;
    
    [Header("Skill Details")]
    [SerializeField] private UI_SkillTreeSLot swordUnlockBtn;
    public bool swordUnlocked{get; private set;}
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private GameObject swordPrefab;
    [FormerlySerializedAs("luchForce")] [SerializeField] private Vector2 luachForce;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float returnSpeed = 10f;
    private Vector2 finalLaunchDir;
    
    [Header("Passive Skill")]
    [SerializeField] private UI_SkillTreeSLot timeStopUnlockBtn;
    public bool timeStopUnlocked{get; private set;}
    [SerializeField] private UI_SkillTreeSLot vulnurableUnlockBtn;
    public bool volnurableUnlocked{get; private set;}
    
    
    [Header("Aim ")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float dotSpacing;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    
    GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
        SetupGravity();
        
        swordUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockSword);
        bounceUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockBounce);
        pierceUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockPierce);
        spinUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockSpin);
        timeStopUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        vulnurableUnlockBtn.GetComponent<Button>().onClick.AddListener(UnlockVolnurable);
        
    }

    private void SetupGravity()
    {
        if(swordType == SwordType.Bounce)
            gravityScale = bounceForce;
        else if(swordType == SwordType.Pierce)
            gravityScale = pierceForce;
        else if(swordType == SwordType.Spin)
            gravityScale = spinForce;
    }

    protected override void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse1))
            finalLaunchDir = new Vector2(AimDirection().normalized.x * luachForce.x, AimDirection().normalized.y * luachForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPos(i * dotSpacing);
            }
        }
    }

    #region Unlock re

    void UnlockSword()
    {
        if (swordUnlockBtn.unlocked)
        {
            swordType = SwordType.Regular;
            swordUnlocked = true;
        }
    }
    
    void UnlockBounce()
    {
        if (bounceUnlockBtn.unlocked)
        {
            swordType = SwordType.Bounce;
            bounceUnlocked = true;
        }
    }
    
    void UnlockPierce()
    {
        if (pierceUnlockBtn.unlocked)
        {
            swordType = SwordType.Pierce;
            pierceUnlocked = true;
        }
    }
    
    void UnlockSpin()
    {
        if (spinUnlockBtn.unlocked)
        {
            swordType = SwordType.Spin;
            spinUnlocked = true;
        }
    }
    
    void UnlockTimeStop()
    {
        if(timeStopUnlockBtn.unlocked)
            timeStopUnlocked = true;
    }
    
    void UnlockVolnurable()
    {
        if(vulnurableUnlockBtn.unlocked)
            volnurableUnlocked = true;
    }
    

    #endregion
    
    
    public void CreateSword()
    {   
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = newSword.GetComponent<Sword_Skill_Controller>();


        if (swordType == SwordType.Bounce)
        {
            gravityScale = bounceForce;
            swordController.SetUpBounce(true, bounceCount,bounceSpeed); 
        }
        else if(swordType == SwordType.Pierce)
            swordController.SetUpPierce(pierceCount);
        else if(swordType == SwordType.Spin)
            swordController.SetUpSpin(true, maxTravelDistance, spinDuration, hitCooldown);
        
        
        swordController.SetUpSword(finalLaunchDir, gravityScale, player, freezeTimeDuration, returnSpeed);
        
        player.AssignSword(newSword);
        
        DotsActive(false);
    }

    #region Aim Region
    public Vector2 AimDirection()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = mousePos - playerPos;
        return aimDir;
    }
    
    public void DotsActive(bool active)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            //Debug.Log("Setting Dots Active");
            dots[i].SetActive(active);
        }
    }
    
    private void GenerateDots()
    {   
//        Debug.Log("Generating Dots");
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPos(float t)
    {
        Vector2 pos = (Vector2) player.transform.position + new Vector2(
            AimDirection().normalized.x * luachForce.x ,
            AimDirection().normalized.y * luachForce.y) * t + 0.5f *(Physics2D.gravity * gravityScale) * (t * t);
        return pos;
    }
    #endregion
}
