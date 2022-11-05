using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class TalkChanger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainText;

    public void Start()
    {
        TalkManager.Instance.OnTextChange.Subscribe(text => { this.mainText.text = text; });
    }
}
