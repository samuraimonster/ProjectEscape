using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageItem : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        itemManager = GameObject.FindWithTag("MainCanvas").GetComponent<RoomManager>().ItemManager;
    }

    public void SendShowItem(int index)
    {
        itemManager.showIten.OnNext(index);
    }

    public void SendAddItem()
    {
        var button = this.gameObject.GetComponent<Button>();
        Destroy(button);
        itemManager.addIten.OnNext(this.gameObject.GetComponent<Image>());
        Destroy(this.gameObject);
    }
}
