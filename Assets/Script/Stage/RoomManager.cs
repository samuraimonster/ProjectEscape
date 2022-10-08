using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Subjects;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Subject<int> changeRoomSubject = new Subject<int>();

    public List<GameObject> buttons = new List<GameObject>();
    
    [Header("•”‰®")]
    public List<RoomEntity> rooms = new List<RoomEntity>();

    public ItemManager ItemManager;

    private int currentRoomIndex = 0;

    private RoomEntity currentRoom;

    private List<GameObject> sides = new List<GameObject>();

    private int currentSideIndex = 0;

    void Start()
    {
        changeRoomSubject.Subscribe(ChangeRoom);

        currentRoom = rooms[currentRoomIndex];
        foreach(var side in currentRoom.sides)
        {
            var ob =Instantiate(side, this.transform.position, Quaternion.identity, this.transform);
            sides.Add(ob);
            ob.SetActive(false);
        }
        sides[currentSideIndex].SetActive(true);
        foreach (var button in buttons)
        {
            button.SetActive(rooms[currentRoomIndex].isVisible);
        }
    }

    public void ChangeSideNext()
    {
        sides[currentSideIndex].SetActive(false);
        currentSideIndex++;
        if (currentSideIndex == sides.Count)
        {
            currentSideIndex = 0;
        }
        sides[currentSideIndex].SetActive(true);
    }

    public void ChangeSideBack()
    {
        sides[currentSideIndex].SetActive(false);
        currentSideIndex--;
        if (currentSideIndex == -1)
        {
            currentSideIndex = sides.Count - 1;
        }
        sides[currentSideIndex].SetActive(true);
    }

    public void ChangeRoom(int index)
    {
        Debug.Log("NEXT_ROOM");
    }
}
