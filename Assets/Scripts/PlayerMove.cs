using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    Animation performance;

    float timer = 0;
    public float attackTime = 1;

	// Use this for initialization
	void Start ()
    {
        Init();
        performance = GetComponentInChildren<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (health <= 0 && !dead)
        {
            dead = true;
            transform.position = transform.position - new Vector3(0, 50, 0);
        }
        else if (attackAnim)
        {
            timer += Time.deltaTime;

            if (timer < attackTime)
            {
                performance.CrossFade("Attack");
            }
            else
            {
                performance.CrossFade("idle");
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
            return;
        }

        if (jumpingUp || fallingDown)
        {
            performance.CrossFade("Jump");
        }
        else if (moving)
        {
            performance.CrossFade("Walk");
        }
        else
        {
            performance.CrossFade("idle");
        }

        if (!moving && !attacking)
        {
            FindSelectableTiles(move);
            CheckMouse();
        }
        else if (moving)
        {
            Move();
        }
        else if (attacking)
        {
            FindSelectableTiles(atkRange);
            CheckMouse();
        }
    }
    
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string targetTag = hit.collider.tag;
                Tile t = null;

                if (targetTag == "NPC" || targetTag == "Player")
                {
                    t = GetTargetTile(hit.collider.gameObject);
                    targetTag = t.tag;
                }
                if (targetTag == "Tile")
                {
                    if (t == null)
                    {
                        t = hit.collider.GetComponent<Tile>();
                    }

                    if (t.selectable && !attacking)
                    {
                        MoveToTile(t);
                    }
                    else if (t.selectable)
                    {
                        Attack(t, "NPC");
                    }
                }
            }
        }
    }
}
