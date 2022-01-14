using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearEffect : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    private Vector2 target;

    private int damage = 10;

    private bool isTouch = false;

    private void Start()
    {
        Invoke("DestroySelf", 3.0f);    
    }

    private void Update()
    {
        if (!isTouch)
        {
            TraceVector();
        }
    }
    private void TraceVector()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }


    public void SetTargetAndDamage(Vector2 target, int damage)
    {
        this.damage = damage;
        this.target = target;

        Vector3 dir = (Vector3)target - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        transform.Rotate(new Vector3(0.0f, 0.0f, -45.0f));
    }

    private void DestroySelf()
    {
        Destroy(gameObject, 1.15f);
        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isTouch == false)
        {
            collision.gameObject.GetComponent<EnemyControl>().EnemyHit(damage);
        }

        isTouch = true;
        StartCoroutine(FadeOut());
        Destroy(gameObject, 1.15f);
    }

    private IEnumerator FadeOut()
    {
        float fadevalue = 1.0f;

        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            fadevalue -= 0.1f;

            gameObject.GetComponent<SpriteRenderer>().color = Color.white * fadevalue;

            if (fadevalue <= 0.0f)
            {
                break;
            }
        }
    }
}
