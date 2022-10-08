using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Scop1,
    Scop2,
    Scop3,
    Key,
    Memo
}


public class ItemEntity : MonoBehaviour
{
    public Image image;
    public ItemType item;

    public void ChangeData(ItemEntity itemEntity)
    {
        image.sprite = itemEntity.image.sprite;
        image = itemEntity.image;
        item = itemEntity.item;
    }
}
