using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    public Transform target, homePosition;
    public float chaseRadius, attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) // Inside Chase Radius but outside Attack Radius
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
