using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<CanvasGroup> canvases = new List<CanvasGroup>();

    public CanvasGroup menuCanvas;

    void Start()
    {
        menuCanvas.alpha = 0;
        menuCanvas.blocksRaycasts = false;
    }

    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickMenu()
    {
        if(menuCanvas.alpha == 0)
        {
            OnMenu();
        }else
        {
            OffMenu();
        }
    }

    private void OnMenu()
    {
        foreach(var group in canvases)
        {
            group.alpha = 0.5f;
            group.blocksRaycasts = false;

        }
        menuCanvas.alpha = 1;
        menuCanvas.blocksRaycasts = true;
    }

    private void OffMenu()
    {
        foreach (var group in canvases)
        {
            group.alpha = 1;
            group.blocksRaycasts = true;

        }
        menuCanvas.alpha = 0;
        menuCanvas.blocksRaycasts = false;
    }
}
