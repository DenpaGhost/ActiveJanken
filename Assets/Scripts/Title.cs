using System;
using UnityEngine;

public class Title : MonoBehaviour
{
    public new Animation animation;
    public AnimationClip close, open;

    public void Start()
    {
        animation.AddClip(close, close.name);
        animation.AddClip(open, open.name);
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