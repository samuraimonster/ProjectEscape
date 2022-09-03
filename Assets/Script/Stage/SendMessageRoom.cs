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

    public void SendChangeRoom(int index)
    {
        roomManager.changeRoomSubject.OnNext(index);
    }
}
