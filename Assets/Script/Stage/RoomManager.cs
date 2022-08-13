using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Subjects;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Subject<int> changeSideSubject = new Subject<int>();

    public Subject<int> changeRoomSubject = new Subject<int>();

    public List<GameObject> buttons = new List<GameObject>();
    
    [Header("•”‰®")]
    public List<RoomEntity> rooms = new List<RoomEntity>();

    private int currentRoomIndex = 0;

    private int currentSideIndex = 0;

    private RoomEntity currentRoom;

    private GameObject currentSide;

    void Start()
    {
        changeSideSubject.Subscribe(index =>
        {
            ChangeSide(index);
        });

        changeRoomSubject.Subscribe(index =>
        {
            ChangeRoom(index);
        });

        currentRoom = rooms[currentRoomIndex];
        currentSide = currentRoom.sides[currentSideIndex];
        currentSide = Instantiate(currentSide);
        currentSide.transform.SetParent(this.transform, false);
        foreach(var button in buttons)
        {
            button.SetActive(rooms[currentRoomIndex].isVisible);
        }
    }

    public void ChangeSideNext()
    {
        Destroy(currentSide);
        if(currentSideIndex == rooms[currentRoomIndex].sides.Count - 1)
        {
            currentSideIndex = 0;
        }
        else
        {
            currentSideIndex++;
        }

        currentSide = currentRoom.sides[currentSideIndex];
        currentSide = Instantiate(currentSide);
        currentSide.transform.SetParent(this.transform, false);
    }

    public void ChangeSideBack()
    {
        Destroy(currentSide);
        if (currentSideIndex == 0)
        {
            currentSideIndex = rooms[currentRoomIndex].sides.Count - 1;
        }
        else
        {
            currentSideIndex--;
        }
        currentSide = currentRoom.sides[currentSideIndex];
        currentSide = Instantiate(currentSide);
        currentSide.transform.SetParent(this.transform, false);
    }

    public void ChangeSide(int index)
    {
        Destroy(currentSide);
        currentSide = currentRoom.sides[index];
        currentSide = Instantiate(currentSide);
        currentSide.transform.SetParent(this.transform, false);
        currentSideIndex = index;
    }

    public void ChangeRoom(int index)
    {
        Destroy(currentSide);
        currentSideIndex = 0;
        currentRoomIndex = index;
        currentRoom = rooms[currentRoomIndex];
        foreach (var button in buttons)
        {
            button.SetActive(rooms[currentRoomIndex].isVisible);
        }
        currentSide = currentRoom.sides[currentSideIndex];
        currentSide = Instantiate(currentSide);
        currentSide.transform.SetParent(this.transform, false);
    }
}
