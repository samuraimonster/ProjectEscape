using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Subject<int> showIten = new Subject<int>();

    [Header("メインキャンバスグループ")]
    public CanvasGroup mainCanvas;

    [Header("UIキャンバスグループ")]
    public CanvasGroup uiCanvas;

    [Header("アイテム拡大表示キャンバスグループ")]
    public CanvasGroup bigItemCanvas;

    [Header("アイテム拡大表示イメージ")]
    public Image bigItemImage;

    [Header("アイテム欄")]
    public List<GameObject> itemBoxies = new List<GameObject>();

    [Header("各アイテム")]
    public List<GameObject> items = new List<GameObject>();

    private List<GameObject> currentItems = new List<GameObject>();

    void Start()
    {
        showIten.Subscribe(ShowItem);
        bigItemCanvas.blocksRaycasts = false;
        bigItemCanvas.alpha = 0;
    }

    void Update()
    {
        
    }

    void ShowItem(int index)
    {
        if(itemBoxies[index].transform.childCount != 0)
        {
            var item = itemBoxies[index].transform.GetChild(0).GetComponent<Image>();
            bigItemImage.sprite = item.sprite;
            bigItemCanvas.blocksRaycasts = true;
            bigItemCanvas.alpha = 1;
            mainCanvas.blocksRaycasts = false;
            mainCanvas.alpha = 0.5f;
            uiCanvas.blocksRaycasts = false;
            uiCanvas.alpha = 0.5f;
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
