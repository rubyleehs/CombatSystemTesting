using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Rigidbody2D))]
public abstract class LiveEntity : MonoBehaviour
{
    [HideInInspector]
    public new Transform transform;
    public Transform headTransform;

    [HideInInspector]
    public Rigidbody2D rb;

    public LiveEntityStats stats;
    public float frictionCoefficient;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    protected SpriteRenderer headSpriteRenderer;

    protected Vector2 velocity;
    
    public float lookAngle;
    protected bool isMovingBackwards = false;

    private float headTiltAngle;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (headTransform != null) headSpriteRenderer = headTransform.GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        animator.SetFloat("Velocity_Horizontal",velocity.x);
        animator.SetFloat("Velocity_Vertical", velocity.y);
        animator.SetBool("isMovingBackwards", isMovingBackwards);
    }

    protected virtual void Move(Vector2 direction)
    {
        if (velocity.sqrMagnitude <= stats.moveSpeed * stats.moveSpeed) velocity += direction.normalized * stats.moveSpeed * rb.mass * frictionCoefficient * GameManager.deltaTime;
        velocity -= velocity.normalized * rb.mass * frictionCoefficient * GameManager.deltaTime;


        if (velocity.sqrMagnitude >= 0.5f)
        {
            rb.velocity = velocity;
        }
        else
        {
            velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
        }

        if (Mathf.Abs(velocity.x) > 0.1f)
        {
            if ((velocity.x > 0.5f && Mathf.Abs(lookAngle) < 90) || (velocity.x < -0.5f && Mathf.Abs(lookAngle) > 90)) isMovingBackwards = false;
            else isMovingBackwards = true;
        }
    }

    protected virtual void Face(Vector2 direction, bool lockHead)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
            headSpriteRenderer.flipX = false;
        }
        else if(direction.x < 0)
        {
            spriteRenderer.flipX = true;
            headSpriteRenderer.flipX = true;
        }

        lookAngle = Vector2.SignedAngle(Vector2.right, direction);
        if (Mathf.Abs(lookAngle) > 90) headTiltAngle = lookAngle - 180;
        else headTiltAngle = lookAngle;

        if (!lockHead)
        {
            headTransform.eulerAngles = new Vector3(0, 0, headTiltAngle);
        }
        else
        {
            headTransform.localRotation = Quaternion.identity;
        }
    }
    

    public void Push(float pushAngle, float pushMomentum)
    {
        Debug.Log(pushAngle);
        velocity += new Vector2(Mathf.Cos(pushAngle * Mathf.Deg2Rad), Mathf.Sin(pushAngle * Mathf.Deg2Rad)) * (pushMomentum / rb.mass);
    }
}
