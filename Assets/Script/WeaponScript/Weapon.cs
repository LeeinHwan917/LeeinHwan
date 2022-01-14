using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    protected CursorImage cursorImage;

    [Header("Stat")]
    [SerializeField]
    protected int damage = 5;
    [SerializeField]
    protected string weaponName = "";
    protected bool isEnemyHit = false;

    [Header("Effect")]
    [SerializeField]
    private GameObject attackEffect;
    [SerializeField]

    private UIControl uiControl;

    private void Start()
    {
        uiControl = GameObject.Find("UI").GetComponent<UIControl>();
        cursorImage = GameObject.Find("CursorImage").GetComponent<CursorImage>();
    }

    private void Update()
    {
        UpdateCursorImage();
    }

    public abstract void Attack();

    protected void UpdateCursorImage()
    {
        isEnemyHit = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        RaycastHit2D[] hit2D = Physics2D.CircleCastAll(mousePos, 0.20f, Vector2.zero);


        for (int i = 0; i < hit2D.Length; i++)
        {
            if (hit2D[i].transform.gameObject.tag == "Enemy")
            {
                isEnemyHit = true;
                cursorImage.SetCursorSprite(1);
            }
        }

        if (isEnemyHit) return;
        else cursorImage.SetCursorSprite(0);
    }

    protected void CreateEffect(Vector2 pos)
    {
        GameObject effect = Instantiate(attackEffect, pos, Quaternion.identity);
        Destroy(effect, 0.4f);
    }

    public void SwapWeapon(Sprite sprite)
    {
        uiControl.SetWeaponImage(sprite);
    }

    public void UpgradeDamage(int damage)
    {
        this.damage += damage;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int ReturnDamage()
    {
        return damage;
    }

    public string ReturnName()
    {
        return weaponName;
    }
}
