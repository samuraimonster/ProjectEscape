using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMarger : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        itemManager = GameObject.Find("ItemCanvas").GetComponent<ItemManager>();
    }

    public void ItemMarge()
    {
        var itemEntity = GetComponent<ItemEntity>();
        switch(itemEntity.item)
        {
            case ItemType.Scop1:
                if (itemManager.GetCurrentItemEntity().item == ItemType.Scop2)
                {
                    itemManager.GetMargeItem(ItemType.Scop3);
                }
                break;
            case ItemType.Scop2:
                if (itemManager.GetCurrentItemEntity().item == ItemType.Scop1)
                {
                    itemManager.GetMargeItem(ItemType.Scop3);
                }
                break;
            case ItemType.Paper:
                itemManager.GetDisassemblyItem(ItemType.Battery);
                break;

        }
    }
}
