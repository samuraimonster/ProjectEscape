using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RoomEntity
{
    public bool isVisible = false;

    public List<GameObject> sides = new List<GameObject>();

    public RoomEntity(bool isVisible, List<GameObject> sides)
    {
        this.isVisible = isVisible;
        this.sides = sides;
    }
}
