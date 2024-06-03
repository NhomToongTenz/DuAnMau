using System.Collections;
using UnityEngine;

namespace Enitity
{
    public class EnityFX : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
    
    [Header("Flash FX")] 
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float flashDuration;
    private Material originalMaterial;
    
    [Header("Ailment colors")]
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;    
    }

    private IEnumerator FlashFX()
    {
        spriteRenderer.material = hitMaterial;
        Color currentColor = spriteRenderer.color;
        
        spriteRenderer.color = Color.white;
        
        
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = currentColor;
        spriteRenderer.material = originalMaterial;
    }
    
    private void RedColorBlink()
    {
        if (spriteRenderer.color != Color.white)
            spriteRenderer.color = Color.white;
        else
            spriteRenderer.color = Color.red;
    }
    
    private void CancleColorChange()
    {
        CancelInvoke();
        spriteRenderer.color = Color.white;
    }

    public void IgniteFxFor(float duration)
    {
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancleColorChange", duration);
    }
    
    public void ShockFxFor(float duration)
    {
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancleColorChange", duration);
    }
    
    public void ChillFxFor(float duration)
    {
        InvokeRepeating("ChillColorFx", 0, 0.3f);
        Invoke("CancleColorChange", duration);
    }
    private void IgniteColorFx()
    {
        if(spriteRenderer.color != igniteColor[0])
            spriteRenderer.color = igniteColor[0];
        else
            spriteRenderer.color = igniteColor[1];
    }
    
    private void ShockColorFx()
    {
        if(spriteRenderer.color != shockColor[0])
            spriteRenderer.color = shockColor[0];
        else
            spriteRenderer.color = shockColor[1];
    }
    public void MakeTransparent(bool isTransparent)
    {
        if(isTransparent)
            spriteRenderer.color = Color.clear;
        else
            spriteRenderer.color = Color.white;
    }
    
    private void ChillColorFx()
    {
        if(spriteRenderer.color != chillColor[0])
            spriteRenderer.color = chillColor[0];
        else
            spriteRenderer.color = chillColor[1];
    }
    }
}