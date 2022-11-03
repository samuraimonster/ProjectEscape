using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class HoldItemBox : MonoBehaviour
{
    [System.NonSerialized]
    public ItemEntity item;

    public Image image;

    public Button button;

    public int index;

}
