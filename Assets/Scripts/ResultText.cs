using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public Text text;
    public new Animation animation;
    public AnimationClip open, close;

    public string Message
    {
        get => text.text;
        set => text.text = value;
    }

    private void Start()
    {
        animation.AddClip(open, open.name);
        animation.AddClip(close, close.name);
    }

    public void Show()
    {
        animation.Play(open.name);
    }

    public void Close()
    {
        animation.Play(close.name);
    }
}