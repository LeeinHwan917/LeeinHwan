using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdomRoom : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private Transform[] stonePositions;
    [SerializeField]
    private GameObject[] stoneObjects;

    [SerializeField]
    private GameObject[] pentagrams;

    [Header("Entity")]
    [SerializeField]
    private GameObject[] enemies;

    [Header("Weapon")]
    [SerializeField]
    private GameObject[] weaponObjects;

    public void RamdomSetRoom(int stage)
    {
        SetEnemy(stage);
        SetEnvironment();
    }

    public void DestroyRoom()
    {
        GameObject[] stone = GameObject.FindGameObjectsWithTag("Stone");

        Destroy(gameObject);
    }

    private void SetEnemy(int stage)
    {
        int enemyCount = Random.Range(stage - 3 <= 0 ? 1 : stage - 3 , stage);

        for (int i = 0; i < enemyCount; i++)
        {
            float RandomX = Random.Range(-15.0f, 15.0f);
            float RandomY = Random.Range(-5.0f, 10.0f);
            int enemyType = Random.Range(0, enemies.Length);

            GameObject enemy = Instantiate(enemies[enemyType], new Vector3(RandomX, RandomY), Quaternion.identity);

            enemy.GetComponent<EnemyControl>().SetHealthPoint(30 + stage * 7);
        }

        GameObject.Find("GameManager").GetComponent<GameManager>().SetSurviveEnemyCount(enemyCount);
    }

    private void SetEnvironment()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomstonetype = Random.Range(0, stoneObjects.Length);
            int randomstonepos = Random.Range(0, stonePositions.Length);
            Instantiate(stoneObjects[randomstonetype], stonePositions[randomstonepos].position, Quaternion.identity, stonePositions[randomstonepos]);
        }

        int pentagramtype = Random.Range(0, pentagrams.Length);
        float RandomX = Random.Range(-3.0f, 3.0f);
        float RandomY = Random.Range(-1.0f, 1.0f);
        Instantiate(pentagrams[pentagramtype], new Vector3(RandomX, RandomY), Quaternion.identity, transform);
    }

    public void DropItem(int stage)
    {
        if (stage % 3 != 0) return;

        int randWeaponValue =Random.Range(0, weaponObjects.Length);

        GameObject randomWeapon = Instantiate(weaponObjects[randWeaponValue], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, transform);

        randomWeapon.GetComponent<Weapon>().SetDamage(stage + 2 * randWeaponValue);
    }
}
