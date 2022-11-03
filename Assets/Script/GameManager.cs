using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasGroup mainCanvas;

    public List<GameObject> sideList;

    void Start()
    {
        var side = Instantiate(sideList[0], mainCanvas.transform.position, Quaternion.identity, mainCanvas.transform);
        side.transform.SetAsFirstSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
