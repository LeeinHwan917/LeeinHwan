using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    private Vector2 target;

    private int damage = 10;

    private bool isComeBack = false;

    public void Update()
    {
        if (!isComeBack)
            TraceVector();
        else
            ComeBackVector();
    }
    private void TraceVector()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (target == (Vector2)transform.position)
        {
            isComeBack = true;
        }
    }

    private void ComeBackVector()
    {
        target = GameObject.Find("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        Vector3 dir = (Vector3)target - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));

        if (target == (Vector2)transform.position)
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetAndDamage(Vector2 target, int damage)
    {
        this.damage = damage;
        this.target = target;

        Vector3 dir = (Vector3)target - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControl>().EnemyHit(damage);
        }
    }
}
