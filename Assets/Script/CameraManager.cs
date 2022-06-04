using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2; 
    public GameObject camera3;
    public GameObject camera4;

    private int count = 1;

    void Start()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
        camera3.SetActive(false);
        camera4.SetActive(false);
    }

   
    void Update()
    {
        
    }

    public void ChangeCameraRight()
    {
        count = count != 4 ? count + 1 : 1;
        
        if (count == 1)
        {
            camera1.SetActive(true);
            camera4.SetActive(false);
        }
        else if(count == 2)
        {
            camera2.SetActive(true);
            camera1.SetActive(false);
            
        }
        else if (count == 3)
        {
            camera3.SetActive(true);
            camera2.SetActive(false);
           
        }
        else if(count == 4)
        {
            camera4.SetActive(true);
            camera3.SetActive(false);
        }
           
    }

    public void ChangeCameraLeft()
    {
        count = count != 1 ? count - 1 : 4;

        if (count == 1)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
        else if (count == 2)
        {
            camera2.SetActive(true);
            camera3.SetActive(false);

        }
        else if (count == 3)
        {
            camera3.SetActive(true);
            camera4.SetActive(false);

        }
        else if (count == 4)
        {
            camera4.SetActive(true);
            camera1.SetActive(false);
        }
    }
}
