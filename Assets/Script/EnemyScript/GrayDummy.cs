using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayDummy : EnemyControl
{
    [SerializeField]
    private float attackDistance = 10.0f;

    [SerializeField]
    private GameObject bulletObject;

    private bool isShot = false;

    private const float shootCoolTime = 2.0f;
    private float shootCoolTimer = shootCoolTime;

    protected override void Attack()
    {
        if (!target) return;

        if (!isShot)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (shootCoolTime <= shootCoolTimer)
            {
                GameObject bullet = Instantiate(bulletObject, transform.position, Quaternion.identity);
                bullet.GetComponent<FireballScript>().SetTarger(target);
                shootCoolTimer = 0.0f;
            }
        }
        CheckDistance();
        shootCoolTimer += Time.deltaTime;
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        isShot = distance <= attackDistance ? true : false;
    }

    public override void SetHealthPoint(int healthPoint)
    {
        this.MaxHealthPoint = healthPoint / 2;
        this.healthPoint = healthPoint / 2;
    }
}
