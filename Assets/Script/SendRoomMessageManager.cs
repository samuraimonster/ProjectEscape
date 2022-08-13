using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendRoomMessageManager : MonoBehaviour
{
    private RoomManager roomManager;

    void Start()
    {
        roomManager = this.transform.parent.GetComponent<RoomManager>();
    }

    public void SendChangeSide(int index)
    {
        roomManager.changeSideSubject.OnNext(index);
    }

    public void SendChangeRoom(int index)
    {
        Debug.Log("room");
        roomManager.changeRoomSubject.OnNext(index);
    }
}
