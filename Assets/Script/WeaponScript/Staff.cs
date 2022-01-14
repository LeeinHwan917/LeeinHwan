using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    public override void Attack()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        RaycastHit2D[] hit2D = Physics2D.CircleCastAll(mousePos, 0.20f, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            CreateEffect(mousePos);
            for (int i = 0; i < hit2D.Length; i++)
            {
                if (hit2D[i].transform.gameObject.tag == "Enemy")
                {
                    hit2D[i].transform.gameObject.GetComponent<EnemyControl>().EnemyHit(damage);
                }
            }
        }
    }
}
