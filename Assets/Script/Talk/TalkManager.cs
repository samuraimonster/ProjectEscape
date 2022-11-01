using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Subjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Subject<string> changeTextSubject = new Subject<string>();

    public IObservable<string> OnTextChanged { get { return changeTextSubject; } }

    public TextMeshProUGUI text;

    private void Start()
    {
        changeTextSubject.Subscribe(ChangeText);
    }

    public void ChangeText(string str)
    {
        text.text = str;
    }
}
