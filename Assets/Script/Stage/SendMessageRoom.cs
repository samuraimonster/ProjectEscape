using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageRoom : MonoBehaviour
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
        roomManager.changeRoomSubject.OnNext(index);
    }
}
