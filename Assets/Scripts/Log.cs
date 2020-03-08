using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidBody;
    public Transform target, homePosition;
    public float chaseRadius, attackRadius;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) // Inside Chase Radius but outside Attack Radius
        {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk)
                && currentState != EnemyState.stagger)
            {
                var tempVector3 = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnim(tempVector3 - transform.position);
                myRigidBody.MovePosition(tempVector3);

                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnim(Vector2 direction)
    {
        if(Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if(Math.Abs(direction.x) > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else
        {
            if (Math.Abs(direction.y) > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else
            {
                SetAnimFloat(Vector2.down);
            }
        }
        Debug.Log(direction);
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }
}
