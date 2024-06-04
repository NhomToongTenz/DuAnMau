using TMPro;
using UnityEngine;

public class BlackHoleHotKeyController : MonoBehaviour
{

    private SpriteRenderer sr;
    private KeyCode hotKey;
    private TextMeshProUGUI hotKeyText;
    private Transform enemyTransform;
    private Blackhole_Skill_Controller blackhole_Skill_Controller;
    
    
    
    public void SetHotKey(KeyCode _hotKey, Transform enemyTransform, Blackhole_Skill_Controller blackhole_Skill_Controller)
    {
        sr = GetComponent<SpriteRenderer>();
        hotKeyText = GetComponentInChildren<TextMeshProUGUI>();
        hotKey = _hotKey;
        hotKeyText.text = hotKey.ToString();
        this.enemyTransform = enemyTransform;
        this.blackhole_Skill_Controller = blackhole_Skill_Controller;
    }
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        
        if (Input.GetKeyDown(hotKey))
        {
            blackhole_Skill_Controller.AddTarget(enemyTransform);
            
            hotKeyText.color = Color.clear;
        }
    }
}
