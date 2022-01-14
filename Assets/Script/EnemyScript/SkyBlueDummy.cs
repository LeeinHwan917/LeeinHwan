using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBlueDummy : EnemyControl
{
    private const float ghostCoolTime = 3.0f;  
    private float ghostCoolTimer = 0.0f;

    public override void SetHealthPoint(int healthPoint)
    {
        float fixedHealthPoint = (float)healthPoint;

        this.MaxHealthPoint = (int)(fixedHealthPoint / 1.5f);
        this.healthPoint = (int)(fixedHealthPoint / 1.5f);
    }

    protected override void Attack()
    {
        if (!target) return;

        ghostCoolTimer += Time.deltaTime;

        if (ghostCoolTime <= ghostCoolTimer)
        {
            StartCoroutine(GhostMode());
            ghostCoolTimer = 0.0f;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private IEnumerator GhostMode()
    {
        Collider2D collider2D = gameObject.GetComponent<Collider2D>();
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        collider2D.tag = "Invincibility";
        collider2D.isTrigger = true;
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        moveSpeed *= 2.0f;

        yield return new WaitForSeconds(1.0f);

        collider2D.tag = "Enemy";
        collider2D.isTrigger = false;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        moveSpeed *= 0.5f;
    }
}
