using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int surviveEnemyCount = 1;

    private bool isClearRoom = false;

    [SerializeField]
    private int floor = 1;

    [SerializeField]
    private GameObject roomObject;

    [SerializeField]
    public GameObject nowRoomObject;

    [SerializeField]
    private Text floorText;

    [Header("if Clear? SetActive")]
    [SerializeField]
    public Text doortext;

    public void SetRoom()
    {
        Destroy(nowRoomObject);
        floor++;
        GameObject room = Instantiate(roomObject, Vector3.zero, Quaternion.identity);
        room.GetComponent<RamdomRoom>().RamdomSetRoom(floor);

        doortext.gameObject.SetActive(false);
        doortext.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        isClearRoom = false;
        nowRoomObject = room;

        floorText.text = "Floor : " + floor.ToString("00");
    }

    public void UpdateClearRoom()
    {
        surviveEnemyCount -= 1;

        if (surviveEnemyCount <= 0)
        {
            nowRoomObject.GetComponent<RamdomRoom>().DropItem(floor);
            doortext.gameObject.SetActive(true);
            doortext.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            isClearRoom = true;
        }
    }

    public void SetSurviveEnemyCount(int count)
    {
        surviveEnemyCount = count;
    }

    public bool ReturnClearSignal()
    {
        return isClearRoom;
    }
}
