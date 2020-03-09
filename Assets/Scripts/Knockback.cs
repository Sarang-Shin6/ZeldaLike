using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        var hasPlayer = player != null;
        var thisIsPlayer = this.GetComponentInParent<PlayerMovement>() != null;

        var enemy = other.GetComponent<Enemy>();
        var hasEnemy = enemy != null;

        //var breakable = other.GetComponent<Breakable>();
        //var targetHasBreakable = breakable != null;

        if (other.gameObject.CompareTag("Breakable") && thisIsPlayer)
        {
            other.GetComponent<Pot>().Smash();
        }
        if (hasEnemy || hasPlayer)
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (hasEnemy && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (hasPlayer)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }

            }
        }
    }

}
