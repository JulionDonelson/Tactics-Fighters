using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    Animator skelAnim;

    float timer = 0;
    float attackTime = 1f;

	// Use this for initialization
	void Start ()
    {
        Init();
        skelAnim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (health <= 0 && !dead)
        {
            dead = true;
            transform.position = new Vector3(0, 50, 0);
        }
        else if (attackAnim)
        {
            timer += Time.deltaTime;

            if (timer < attackTime)
            {
                skelAnim.SetBool("isAttacking", true);
            }
            else
            {
                skelAnim.SetBool("isAttacking", false);
                attackAnim = false;
                timer = 0;
            }
        }

        if (!turn)
        {
            return;
        }
        else if (dead)
        {
            TurnManager.EndTurn();
            // TurnManager.RemoveUnit(this);
            // Destroy(this.gameObject);
            return;
        }

        if (jumpingUp || fallingDown)
        {
            skelAnim.SetBool("isGrounded", false);
        }
        else
        {
            skelAnim.SetBool("isGrounded", true);
        }

        if (moving)
        {
            skelAnim.SetBool("isWalking", true);
        }
        else
        {
            skelAnim.SetBool("isWalking", false);
        }

        if (!moving && !attacking)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles(move);
            actualTargetTile.target = true;
        }
        else if (moving)
        {
            Move();
        }
        else if (attacking)
        {
            FindNearestTarget();
            CalculateAttackPath();
            FindSelectableTiles(atkRange);
            actualTargetTile.target = true;
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile, move);
    }

    void CalculateAttackPath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile, atkRange);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
