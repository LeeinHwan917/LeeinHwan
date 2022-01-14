using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDummy : EnemyControl
{
    protected override void Attack()
    {
        if (!target) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public override void SetHealthPoint(int healthPoint)
    {
        this.MaxHealthPoint = healthPoint;
        this.healthPoint = healthPoint;
    }
}
