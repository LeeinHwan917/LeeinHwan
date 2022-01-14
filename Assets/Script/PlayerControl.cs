using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private UIControl uiControl;
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Transform teleportPos;

    [Header("MoveInfo")]
    [SerializeField]
    private float moveSpeed = 10.0f;

    private bool isNorth = false;
    private bool isSouth = false;
    private bool isWest = false;
    private bool isEast = false;
    
    [Header("Stat")]
    private const int maxHealthPoint = 10;
    private int healthPoint = maxHealthPoint;

    private const float i_frameTime = 1.0f; // 무적시간
    private float i_frameTimer = i_frameTime; // 무적시간

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        i_frameTimer += Time.deltaTime;
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);

        if ((h > 0 && isEast == true) || (h < 0 && isWest == true))
        {
            h = 0;
        }

        if ((v > 0 && isNorth == true) || (v < 0 && isSouth == true))
        {
            v = 0;
        }

        transform.Translate(new Vector3(h, v, 0.0f) * Time.deltaTime * moveSpeed);
    }

    // 데미지 없음. -> 스테이지에 따라서 데미지가 증가하는 방식 *Isaac
    private void PlayerHit(int damage)
    {
        if (healthPoint == 0) return;

        healthPoint -= damage;
        i_frameTimer = 0.0f;

        StartCoroutine(IframeEffect());    

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
            //gameOver;
        }

        uiControl.UpdateHealthBar(healthPoint);
    }
    private void PlayerHeal(int healAmount)
    {
        healthPoint += healAmount;
    }

    private IEnumerator IframeEffect()
    {
        while (i_frameTime > i_frameTimer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.5f);

            yield return new WaitForSeconds(0.15f);

            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            yield return new WaitForSeconds(0.15f);
        }

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Border":
                if (collision.gameObject.name == "NorthCollider")
                    isNorth = true;
                else if (collision.gameObject.name == "SouthCollider")
                    isSouth = true;

                if (collision.gameObject.name == "WestCollider")
                    isWest = true;
                else if (collision.gameObject.name == "EastCollider")
                    isEast = true;
                break;

            case "Door":
                if (gameManager.ReturnClearSignal())
                {
                    uiControl.SetActiveFadeDoorText(true);
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
            case "Fireball":
                if (i_frameTime <= i_frameTimer)
                {
                    PlayerHit(1);
                }
                break;

            case "Door":
                if (gameManager.ReturnClearSignal() == true && Input.GetKey(KeyCode.E) && !isNorth)
                {
                    gameManager.SetRoom();
                    transform.position = teleportPos.position;
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Border":
                if (collision.gameObject.name == "NorthCollider")
                    isNorth = false;
                else if (collision.gameObject.name == "SouthCollider")
                    isSouth = false;

                if (collision.gameObject.name == "WestCollider")
                    isWest = false;
                else if (collision.gameObject.name == "EastCollider")
                    isEast = false;
                break;

            case "Door":
                if (gameManager.ReturnClearSignal())
                {
                    uiControl.SetActiveFadeDoorText(false);
                }
                break;
        }
    }
}
