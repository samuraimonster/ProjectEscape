using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using System;

public class TalkManager : MonoBehaviour
{
    private Subject<string> textChangeSubject = new();

    public static TalkManager Instance;

    public IObservable<string> OnTextChange
    {
        get { return textChangeSubject; }
    }

    public void TextChange(string text)
    {
        textChangeSubject.OnNext(text);
    }
    private void Awake()
    {
        Instance = this;
    }

}
