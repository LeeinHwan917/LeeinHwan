using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float moveSpeed = 10.0f;

    private bool isHit = false;

    private void Start()
    {
        DestroySelf(3.0f);
    }

    private void Update()
    {
        if (!isHit) Trace();
    }
    public void SetTarger(Transform target)
    {
        this.target = target;
    }
    private void Trace()
    {
        if (!target) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && !isHit)
            DestroySelf(0.0f);
    }

    private void DestroySelf(float time)
    {
        Destroy(gameObject, time + 1.0f);
        StartCoroutine(FadeOut(time));
    }

    private IEnumerator FadeOut(float time)
    {
        yield return new WaitForSeconds(time);

        isHit = true;
        float value = 1.0f;

        while(true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(value, value, value, value);

            yield return new WaitForSeconds(0.1f);

            value -= 0.1f;

            if (value <= 0.0f) break;
        }
    }
}
