using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Subject<int> showIten = new Subject<int>();

    [Header("���C���L�����o�X�O���[�v")]
    public CanvasGroup mainCanvas;

    [Header("UI�L�����o�X�O���[�v")]
    public CanvasGroup uiCanvas;

    [Header("�A�C�e���g��\���L�����o�X�O���[�v")]
    public CanvasGroup bigItemCanvas;

    [Header("�A�C�e���g��\���C���[�W")]
    public Image bigItemImage;

    [Header("�A�C�e����")]
    public List<GameObject> itemBoxies = new List<GameObject>();

    [Header("�e�A�C�e��")]
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
