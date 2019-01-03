using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Rigidbody2D))]
public abstract class LiveEntity : MonoBehaviour
{
    [HideInInspector]
    public new Transform transform;
    [HideInInspector]
    public Rigidbody2D rb;

    public float moveSpeed;//future swap to moveLogic?
    public float frictionCoefficient;

    protected Vector2 velocity;
    protected float lookAngle;

    protected virtual void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    protected virtual void Move(Vector2 direction)
    {
        if(velocity.sqrMagnitude <= moveSpeed*moveSpeed) velocity += direction.normalized * moveSpeed * rb.mass * frictionCoefficient * GameManager.deltaTime;
        velocity -= velocity.normalized * rb.mass * frictionCoefficient * GameManager.deltaTime;
        if (velocity.sqrMagnitude >= 0.5f) rb.velocity = velocity;
        else rb.velocity = Vector2.zero;
    }

    protected virtual void Look(Vector2 direction)
    {
        lookAngle = Mathf.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
        if (direction.x < 0) lookAngle += 180;
        transform.eulerAngles = new Vector3(0, 0, lookAngle);
    }

}
