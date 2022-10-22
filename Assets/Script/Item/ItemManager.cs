using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Subject<int> showIten = new Subject<int>();

    public Subject<ItemEntity> addIten = new Subject<ItemEntity>();

    [Header("メインキャンバスグループ")]
    public CanvasGroup mainCanvas;

    [Header("UIキャンバスグループ")]
    public CanvasGroup uiCanvas;

    [Header("アイテム拡大表示キャンバスグループ")]
    public CanvasGroup bigItemCanvas;

    [Header("アイテム拡大表示イメージ")]
    public Image bigItemImage;

    [Header("アイテム欄")]
    public List<ItemData> itemBoxies;

    [System.NonSerialized]
    public List<ItemEntity> itemList = new();

    public List<ItemEntity> margeItemList;

    private int currentAddIndex = 0;

    private int selectIndex = -1;

    private int selectBigIndex = -1;

    void Start()
    {
        showIten.Subscribe(ShowItem);
        addIten.Subscribe(AddItem);

        bigItemCanvas.blocksRaycasts = false;
        bigItemCanvas.alpha = 0;
    }
    public void GetDisassemblyItem(ItemType item)
    {
        ItemEntity itemEntity = null;
        foreach (var margeItem in margeItemList)
        {
            if (margeItem.item == item)
            {
                itemEntity = margeItem;
                break;
            }
        }

        bigItemImage.sprite = itemEntity.image.sprite;
        var entiry = bigItemImage.GetComponent<ItemEntity>();
        entiry.image = itemEntity.image;
        entiry.item = itemEntity.item;

        itemList[selectBigIndex].ChangeData(itemEntity);
    }
    public void GetMargeItem(ItemType item)
    {
        ItemEntity itemEntity = null;
        foreach(var margeItem in margeItemList)
        {
            if(margeItem.item == item)
            {
                itemEntity = margeItem;
                break;
            }
        }

        bigItemImage.sprite = itemEntity.image.sprite;
        var entiry = bigItemImage.GetComponent<ItemEntity>();
        entiry.image = itemEntity.image;
        entiry.item = itemEntity.item;

        itemList[selectBigIndex].ChangeData(itemEntity);
        Destroy(itemList[selectIndex].gameObject);
        itemList.RemoveAt(selectIndex);

        itemBoxies[selectIndex].itenBox.GetComponent<Image>().color = Color.white;
        itemBoxies[selectIndex].isSelect = false;
        selectIndex = selectBigIndex;
        itemBoxies[selectBigIndex].itenBox.GetComponent<Image>().color = Color.blue;
        itemBoxies[selectBigIndex].isSelect = true;

        currentAddIndex = itemList.Count;
    }

    public ItemEntity GetCurrentItemEntity()
    {
        if(selectIndex == -1)
        {
            return null;
        }

        if(itemList.Count == 0)
        {
            return null;
        }

        return itemList[selectIndex];
    }

    void AddItem(ItemEntity itemEntity)
    {   
        var ob = Instantiate(itemEntity, itemBoxies[currentAddIndex].itenBox.transform.position, Quaternion.identity, itemBoxies[currentAddIndex].itenBox.transform);
        itemList.Add(ob);
        currentAddIndex++;
    }

    void ShowItem(int index)
    {
        if(itemBoxies[index].itenBox.transform.childCount != 0)
        {
            if (itemBoxies[index].isSelect)
            {
                var item = itemList[index];
                bigItemImage.sprite = item.image.sprite;
                var entiry = bigItemImage.GetComponent<ItemEntity>();
                entiry.image = item.image;
                entiry.item = item.item;


                bigItemCanvas.blocksRaycasts = true;
                bigItemCanvas.alpha = 1;

                mainCanvas.blocksRaycasts = false;
                mainCanvas.alpha = 0.5f;

                uiCanvas.blocksRaycasts = false;
                uiCanvas.alpha = 0.5f;
                selectBigIndex = index;
            }
            else
            {
                if(selectIndex >= 0)
                {
                    if(selectIndex == index)
                    {
                        itemBoxies[selectIndex].itenBox.GetComponent<Image>().color = Color.white;
                        itemBoxies[selectIndex].isSelect = false;
                        selectIndex = -1;
                    }
                    else
                    {
                        itemBoxies[selectIndex].itenBox.GetComponent<Image>().color = Color.white;
                        itemBoxies[selectIndex].isSelect = false;
                        itemBoxies[index].itenBox.GetComponent<Image>().color = Color.blue;
                        itemBoxies[index].isSelect = true;
                        selectIndex = index;
                    }
                }
                else
                {
                    itemBoxies[index].itenBox.GetComponent<Image>().color = Color.blue;
                    itemBoxies[index].isSelect = true;
                    selectIndex = index;
                }
            }
        }
    }

    public void CloseItem()
    {
        bigItemCanvas.blocksRaycasts = false;
        bigItemCanvas.alpha = 0;

        mainCanvas.blocksRaycasts = true;
        mainCanvas.alpha = 1f;

        uiCanvas.blocksRaycasts = true;
        uiCanvas.alpha = 1f;

        selectBigIndex = -1;
    }
}
