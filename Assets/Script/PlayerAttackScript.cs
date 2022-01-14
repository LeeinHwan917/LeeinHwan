using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField]
    private Weapon weapon;

    [Header("UI")]
    [SerializeField]
    private UIControl uiControl;

    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Text weaponDamageText;
    [SerializeField]
    private Text infoText;

    private void Start()
    {
        uiControl.SetWeaponText(weapon.ReturnName(), weapon.ReturnDamage());
    }

    void Update()
    {
        weapon.Attack();
        ScanWeaponRayCast();
    }
    private void ScanWeaponRayCast()
    {
        bool isWeapon = false;

        RaycastHit2D[] hit2D = Physics2D.CircleCastAll(transform.position, 1.0f, Vector2.zero);

        for (int i = 0; i < hit2D.Length; i++)
        {
            if (hit2D[i].transform.gameObject.tag == "Weapon")
            {
                isWeapon = true;
                Weapon weapon = hit2D[i].transform.gameObject.GetComponent<Weapon>();

                SetText(true, weaponNameText, weapon.ReturnName());
                SetText(true, weaponDamageText, "damage " + weapon.ReturnDamage().ToString());
                SetText(true, infoText , "Press 'R'");

                if (Input.GetKeyDown(KeyCode.R))
                {
                    SwapWeapon(weapon); 
                }
            }
        }

        if (!isWeapon)
        {
            SetText(false, weaponNameText);
            SetText(false, weaponDamageText);
            SetText(false, infoText);
        }
    }

    private void SwapWeapon(Weapon weapon)
    {
        Destroy(this.weapon.gameObject);

        this.weapon = weapon;

        this.weapon.gameObject.transform.position = new Vector3(30.0f, 30.0f, 0.0f);
        this.weapon.gameObject.transform.parent = transform;

        uiControl.SetWeaponText(weapon.ReturnName(), weapon.ReturnDamage());

        weapon.SwapWeapon(weapon.GetComponent<SpriteRenderer>().sprite);
    }

    private void SetText(bool isTrue, Text text1, string text = "")
    {
        text1.text = text;
        float value = isTrue == true ? 1.0f : 0.0f;

        text1.color = Color.white * value;
    }
}
