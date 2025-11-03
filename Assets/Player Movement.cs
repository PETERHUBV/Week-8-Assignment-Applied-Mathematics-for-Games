using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane")]
    public float speedoflane;
    public int currentlane;
    public float laneDistance;


    [Header("Movement")]
    public float JumpHeight;
    public float JumpDuration;
    public bool isJumping;
    public float JumpTimer;
    public float GroundY;

    public Animator animator;

    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        GroundY = transform.position.y;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateJump();
        SmoothMoveToLane();
    }
   public void HandleInput()
    {
        
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && !isJumping)
        {
            currentlane = Mathf.Max(0, currentlane - 1);
            if (animator) animator.SetTrigger("MoveLeft");
            SetTargetLane();
        }

    
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && !isJumping)
        {
            currentlane = Mathf.Min(2, currentlane + 1);
            if (animator) animator.SetTrigger("MoveRight");
            SetTargetLane();
        }

       
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !isJumping)
        {
            isJumping = true;
            JumpTimer = 0f;
            if (animator) animator.SetTrigger("Jump");
        }
    }

    public void SetTargetLane()
    {
        float targetX = (currentlane - 1) * laneDistance;
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
    }

   public  void SmoothMoveToLane()
    {


        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speedoflane);
    }

    public void UpdateJump()
    {
        if (isJumping)
        {
            JumpTimer += Time.deltaTime;
            float t = JumpTimer / JumpDuration;

            if (t <= 1f)
            {
                float JumpY = Mathf.Sin(t * Mathf.PI) * JumpHeight;
                transform.position = new Vector3(transform.position.x, GroundY + JumpY, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, GroundY, transform.position.z);
                isJumping = false;
                if (animator) animator.SetTrigger("Run");
            }
        }
    }
}