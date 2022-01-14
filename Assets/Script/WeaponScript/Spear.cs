using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    [SerializeField]
    private GameObject spearEffect;

    private Vector2 targetPos;
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = GameObject.Find("Player").transform.position;

            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            GameObject spear = Instantiate(spearEffect, pos, Quaternion.identity);
            spear.GetComponent<SpearEffect>().SetTargetAndDamage(targetPos, damage);
        }
    }
}
