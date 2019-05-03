using System;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

public class RpsButton : MonoBehaviour
{
    public Action<IRpsButtonClickListener> listener;
    public Player player;

    public virtual void OnClick()
    {
        listener();
    }
}