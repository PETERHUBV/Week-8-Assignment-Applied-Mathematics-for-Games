using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHP = 100;
    public int currentHP;
    public float RegenRate = 1f;   
   public float RegenTimer = 0f;


    [Header("Hit")]
    public List<Vector3> enemyPositions = new List<Vector3>(); 
    public float HitRangeZ = 0.3f;
    public float LaneToleranceX = 0.5f;
    public float ForwardSpeed = 2f;


    public PlayerMovement playerMovement;
    public Text HPText;            

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText();


        enemyPositions.Add(new Vector3(-playerMovement.laneDistance, transform.position.y, 3f));
        enemyPositions.Add(new Vector3(0f, transform.position.y, 6f));
        enemyPositions.Add(new Vector3(playerMovement.laneDistance, transform.position.y, 9f));

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
    public void MoveForward()
    {
        transform.position += Vector3.forward * ForwardSpeed * Time.deltaTime;
    }
    public void RegenerateHealth()
    {
        if (currentHP < maxHP)
        {
            RegenTimer += Time.deltaTime;
            if (RegenTimer >= 1f)
            {
                RegenTimer = 0f;
                Heal(1);
            }
        }
    }
    public void CheckHits()
    {
        foreach (Vector3 enemy in enemyPositions)
        {
            float zDiff = Mathf.Abs(transform.position.z - enemy.z);
            float xDiff = Mathf.Abs(transform.position.x - enemy.x);

            if (zDiff < HitRangeZ && xDiff < LaneToleranceX)
            {
                if (!playerMovement.isJumping)
                {
                    TakeDamage(5);
                    Debug.Log("Obstacle!");
                }
                else
                {
                    Debug.Log("Pass obstacle!");
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
      
        currentHP = Mathf.Max(0, currentHP - amount);
        UpdateHPText();

        if (currentHP <= 0)
        {
            Debug.Log("Where is Player");
        }
    }

    public void Heal(int amount)
    {
       
        currentHP = Mathf.Min(maxHP,currentHP + amount);
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        if (HPText != null)
            HPText.text = "HP: " + currentHP;
    }
}