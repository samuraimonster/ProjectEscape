using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemEntity itemEntity;

    public Button button;

    private void Start()
    {
        button.onClick.AddListener(() =>
        { 
            ItemManager.Instance.Add(itemEntity); 
            Destroy(gameObject); 
        });
    }
}