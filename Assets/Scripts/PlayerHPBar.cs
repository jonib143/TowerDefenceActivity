using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    [Header("UI References")]
    public Image realHpBar;   
    public Image ghostHpBar;  

    [Header("HP Settings")]
    public float maxHp = 20f;
    private float currentHp;

    [Header("Ghost Bar Animation")]
    public float delayBeforeEase = 0.4f; 
    public float easeDuration = 0.6f;    

    private Coroutine ghostCoroutine;

    private void Start()
    {
        currentHp = maxHp;
        UpdateHpBarsInstant();
    }

    private void Update()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        currentHp = Mathf.Clamp(currentHp - damageAmount, 0f, maxHp);
        float targetFill = currentHp / maxHp;
        
        realHpBar.fillAmount = targetFill;
        
        if (ghostCoroutine != null)
        {
            StopCoroutine(ghostCoroutine);
        }
        ghostCoroutine = StartCoroutine(AnimateGhostBar(targetFill));
    }
    
    private IEnumerator AnimateGhostBar(float targetFill)
    {
        yield return new WaitForSeconds(delayBeforeEase);

        float initialFill = ghostHpBar.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < easeDuration)
        {
            elapsedTime += Time.deltaTime;
            float rawT = Mathf.Clamp01(elapsedTime / easeDuration);
            
            float easedT = Ease.EaseOutQuad(rawT);
            
            ghostHpBar.fillAmount = Mathf.Lerp(initialFill, targetFill, easedT);

            yield return null;
        }

        ghostHpBar.fillAmount = targetFill;
    }

    private void UpdateHpBarsInstant()
    {
        float fill = currentHp / maxHp;
        realHpBar.fillAmount = fill;
        ghostHpBar.fillAmount = fill;
    }
}