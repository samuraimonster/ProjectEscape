using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HoldItemEntity 
{
    [System.NonSerialized]
    public ItemEntity item;

    public int index;

    public HoldItemEntity(ItemEntity item, int index)
    {
        this.item = item;
        this.index = index;
    }
}
