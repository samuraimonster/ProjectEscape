using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemList;

    public static ItemManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Itemtype
    {
        Scop1,
        Scop2,
        Scop3,
        Paper,
        Battery,
        Key
    }

    private Subject<ItemEntity> itemAddSubject = new();
    private Subject<ItemEntity> itemDeleteSubject = new();
    private Subject<HoldItemEntity> itemChangeSubject = new();

    public IObservable<ItemEntity> OnItemAdd
    {
        get { return itemAddSubject; }
    }

    public IObservable<ItemEntity> OnItemDelete
    {
        get { return itemDeleteSubject; }
    }

    public IObservable<HoldItemEntity> OnItemChange
    {
        get { return itemChangeSubject; }
    }

    public void Add(ItemEntity item)
    {
        itemAddSubject.OnNext(item);
    }

    public void Delete(ItemEntity item)
    {
        itemDeleteSubject.OnNext(item);
    }

    public void Change(HoldItemEntity item)
    {
        itemChangeSubject.OnNext(item);
    }

    public ItemEntity ItemCheck(Itemtype bigItemtype, Itemtype itemtype)
    {
        if(bigItemtype == Itemtype.Scop1 && itemtype == Itemtype.Scop2)
        {
            return itemList[(int)Itemtype.Scop3].itemEntity;
        }
        else if (bigItemtype == Itemtype.Paper)
        {
            return itemList[(int)Itemtype.Battery].itemEntity;
        }
        return null;
    }
}
