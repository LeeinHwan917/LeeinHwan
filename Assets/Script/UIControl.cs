using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private float playTime;
    private const int sexagesimal = 60;

    [SerializeField]
    private GameManager gameManager;

    [Header("Image")]
    [SerializeField]
    private Image[] hpbars;
    [SerializeField]
    private Image weaponImage;

    [Header("Text")]
    [SerializeField]
    private Text doorText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text enemyCountText;

    [SerializeField]
    private Text weaponNameText;
    [SerializeField]
    private Text weaponDamageText;

    private void Update()
    {
        playTime += Time.deltaTime;
        UpdatePlayTime();
        UpdateEnemyCountText();
    }

    public void UpdateHealthBar(int curHealth)
    {
        for (int i = 0; i < hpbars.Length; i++)
        {
            hpbars[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < curHealth; i++)
        {
            hpbars[i].gameObject.SetActive(true);
        }
    }

    public void UpdateEnemyCountText()
    {
        enemyCountText.text = "Alive : " + gameManager.surviveEnemyCount.ToString("00");
    }

    private void UpdatePlayTime()
    {
        int min = (int)playTime / sexagesimal;
        int sec = (int)playTime % sexagesimal;

        timeText.text = min.ToString("00") + " : " + sec.ToString("00");
    }

    public void SetWeaponImage(Sprite sprite)
    {
        weaponImage.sprite = sprite;
    }

    public void SetActiveFadeDoorText(bool isTrue)
    {
        StartCoroutine(FadeOutText(isTrue, doorText));
    }

    public void SetWeaponText(string weaponName , int weaponDamage)
    {
        weaponNameText.text = weaponName;
        weaponDamageText.text = "damage " + weaponDamage.ToString("00");
    }

    public IEnumerator FadeOutText(bool isTrue , Text targetText)
    {
        float target_value = isTrue == true ? 1.0f : 0.0f;

        float cur_value = isTrue == true ? 0.0f : 1.0f;

        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            if (isTrue)
                cur_value += 0.05f;

            else
                cur_value -= 0.05f;

            targetText.color = Color.white * cur_value;

            if (isTrue && target_value <= cur_value) break; 
            if (!isTrue && target_value >= cur_value) break;
        }
    }
}
