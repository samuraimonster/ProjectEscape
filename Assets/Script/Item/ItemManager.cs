using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Subject<int> showIten = new Subject<int>();

    public Subject<Image> addIten = new Subject<Image>();

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

    private int currentAddIndex = 0;

    private int selectIndex = -1;

    void Start()
    {
        showIten.Subscribe(ShowItem);
        addIten.Subscribe(AddItem);
        bigItemCanvas.blocksRaycasts = false;
        bigItemCanvas.alpha = 0;
    }

    void AddItem(Image image)
    {
        var ob = Instantiate(image, itemBoxies[currentAddIndex].itenBox.transform.position, Quaternion.identity, itemBoxies[currentAddIndex].itenBox.transform);
        currentAddIndex++;
    }

    void ShowItem(int index)
    {
        if(itemBoxies[index].itenBox.transform.childCount != 0)
        {
            if (itemBoxies[index].isSelect)
            {
                var item = itemBoxies[index].itenBox.transform.GetChild(0).GetComponent<Image>();
                bigItemImage.sprite = item.sprite;
                bigItemCanvas.blocksRaycasts = true;
                bigItemCanvas.alpha = 1;
                mainCanvas.blocksRaycasts = false;
                mainCanvas.alpha = 0.5f;
                uiCanvas.blocksRaycasts = false;
                uiCanvas.alpha = 0.5f;
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
    }
}
