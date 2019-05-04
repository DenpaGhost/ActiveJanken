using System;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RpsButton : MonoBehaviour
{
    public Player player;
    public Sukumi sukumi;
    public Image image;
    public RpsButtonOnClickHandler handler;

    public Sprite SukumiIcon => image.sprite;


    public void OnClick()
    {
        handler.OnClick(this);
    }
}