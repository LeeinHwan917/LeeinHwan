using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyControl : MonoBehaviour
{
    [SerializeField]
    private GameObject dieEffect;
    [SerializeField]
    private GameObject tombStone;

    [SerializeField]
    protected Transform target;

    [Header("Stat")]
    protected int MaxHealthPoint = 30;
    [SerializeField]
    protected int healthPoint = 30;
    [SerializeField]
    protected float moveSpeed = 5.0f;

    [Header("HP_BAR")]
    [SerializeField]
    private Image hpbarImage;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Attack();
    }
    protected abstract void Attack();

    public abstract void SetHealthPoint(int healthPoint);
    public void EnemyHit(int damage)
    {
        if (healthPoint == 0 || !dieEffect) return;

        healthPoint -= damage;

        if (healthPoint <= 0)
        {
            healthPoint = 0;
            EnemyDie();
        }

        hpbarImage.fillAmount = (float)healthPoint / (float)MaxHealthPoint; 
    }
    private void EnemyDie()
    {
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Instantiate(tombStone, transform.position, Quaternion.identity, GameObject.Find("GameManager").GetComponent<GameManager>().nowRoomObject.transform);

        Destroy(gameObject);
        Destroy(effect, 0.5f);

        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateClearRoom();
    }

}
