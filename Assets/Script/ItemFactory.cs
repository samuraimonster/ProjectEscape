using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ItemFactory : MonoBehaviour
{
    [SerializeField]
    private ItemViewer itemViewer;

    private List<ItemEntity> holdItemList = new();

    private void Start()
    {
        ItemManager.Instance.OnItemAdd.Subscribe(item =>
        {
            holdItemList.Add(item);

            itemViewer.UpdateView(holdItemList);
        });

        ItemManager.Instance.OnItemDelete.Subscribe(item =>
        {
            holdItemList.Remove(item);
            itemViewer.UpdateView(holdItemList);
        });
    }


}
