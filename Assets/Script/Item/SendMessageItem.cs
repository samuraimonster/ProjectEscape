using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageItem : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        itemManager = this.transform.parent.GetComponent<ItemManager>();
    }

    public void SendShowItem(int index)
    {
        itemManager.showIten.OnNext(index);
    }
}
