using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private RoomManager roomManager;

    private ItemManager itemManager;

    private bool isOpen = false;
    void Start()
    {
        roomManager = this.transform.parent.GetComponent<RoomManager>();
        itemManager = GameObject.Find("ItemCanvas").GetComponent<ItemManager>();
    }

    public void SendChangeRoom(int index)
    {
        var itemEntity = itemManager.GetCurrentItemEntity();
        if (itemEntity == null)
        {
            return;
        }

        if(isOpen)
        {
            roomManager.changeRoomSubject.OnNext(index);
        }

        if(itemEntity.item == ItemType.Key)
        {
            isOpen = true;
        }

    }
}
