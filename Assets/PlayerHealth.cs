using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{

    public int maxHP = 100;
    public int currentHP;
    public float RegenRate = 1f;   
   public float RegenTimer = 0f;

    public Text HPText;            

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();
    }

    void Update()
    {
        
        RegenTimer += Time.deltaTime;
        if (RegenTimer >= 1f)
        {
            RegenTimer = 0f;
            Heal((int)RegenRate);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(0, currentHP);
        UpdateHPText();
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP);
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        if (HPText != null)
            HPText.text = "HP: " + currentHP.ToString();
    }
}