using System;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

public class RpsButton : MonoBehaviour
{
    public IRpsButtonClickListener listener;
    public Player player;

    protected virtual void OnClick()
    {
        listener?.OnClick(this);
    }
}