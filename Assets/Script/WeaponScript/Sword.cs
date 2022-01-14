using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField]
    private GameObject swordEffect;

    private Vector2 targetPos;

    private bool isCoolTime = false;

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isCoolTime)
        {
            Vector3 pos = GameObject.Find("Player").transform.position;

            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            GameObject sword = Instantiate(swordEffect, pos, Quaternion.identity);
            sword.GetComponent<SwordEffect>().SetTargetAndDamage(targetPos, damage);

            StartCoroutine(CheckDestroySwordObject(sword));

            isCoolTime = true;
        }
    }

    private IEnumerator CheckDestroySwordObject(GameObject sword)
    {
        while(true)
        {
            if (sword)
                yield return new WaitForEndOfFrame();
            else if (!sword)
                break;
        }

        isCoolTime = false;
    }
}
