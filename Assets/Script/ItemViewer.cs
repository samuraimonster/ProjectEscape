using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField]
    private List<HoldItemBox> holdItemBoxList;

    [SerializeField]
    private List<Image> imageList;

    [SerializeField]
    private CanvasGroup bigItemCanvas;

    [SerializeField]
    private Button bigItemCloseButton;

    [SerializeField]
    private Image bigImage;

    [SerializeField]
    private Button bigItemButton;

    private int selectIndex = -1;

    private int bigSelectIndex = -1;

    private void Start()
    {
        foreach (var item in holdItemBoxList)
        {
            item.button.onClick.AddListener(() => 
            {
                Select(item.index);
            });
        }

        bigItemCloseButton.onClick.AddListener(() => 
        {
            Visible(false);
        });

        bigItemButton.onClick.AddListener(() =>
        {
            Debug.Log(holdItemBoxList[bigSelectIndex].item.itemtype);
            Debug.Log(holdItemBoxList[selectIndex].item.itemtype);
            var item = ItemManager.Instance.ItemCheck(holdItemBoxList[bigSelectIndex].item.itemtype, holdItemBoxList[selectIndex].item.itemtype);
            if (item == null) return;

            ItemManager.Instance.Delete(holdItemBoxList[selectIndex].item);
            ItemManager.Instance.Delete(holdItemBoxList[bigSelectIndex].item);
            ItemManager.Instance.Add(item.itemEntity);
            Select(bigSelectIndex);
            Visible(true);
        });
    }

    public void Select(int index)
    {
        if(index != selectIndex)
        {
            if (holdItemBoxList[index].item == null) return;

            if(selectIndex != -1) holdItemBoxList[selectIndex].image.color = Color.white;

            selectIndex = index;
            holdItemBoxList[selectIndex].image.color = Color.blue;
        }
        else
        {
            Visible(true);
            bigSelectIndex = index;
        }
    }

    public void UpdateView(List<ItemEntity> itemList)
    {
        for(int i = 0; i < holdItemBoxList.Count; i++)
        {
            if(i < itemList.Count)
            {
                holdItemBoxList[i].item = itemList[i];
            }
            else
            {
                holdItemBoxList[i].item = null;
            }
        }
    }

    private void Visible(bool isVisible)
    {
        if (isVisible)
        {
            bigItemCanvas.alpha = 1;
            bigItemCanvas.blocksRaycasts = true;
            bigImage.sprite = holdItemBoxList[selectIndex].item.image.sprite;
        }
        else
        {
            bigItemCanvas.alpha = 0;
            bigItemCanvas.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < holdItemBoxList.Count; i++)
        {
            if (holdItemBoxList[i].item == null)
            {
                imageList[i].enabled = false;
                imageList[i].sprite = null;
                continue;
            }

            if(holdItemBoxList[i].item.image != null)
            {
                imageList[i].enabled = true;
                imageList[i].sprite = holdItemBoxList[i].item.image.sprite;
            }
        }
    }
}
