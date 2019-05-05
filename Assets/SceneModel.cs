using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class SceneModel : MonoBehaviour
{
    public RpsButton
        aRock, aScissors, aPaper, bRock, bScissors, bPaper;

    public Image aPickupSukumi, bPickupSukumi;
    public Text aText, bText;
    public Sprite blankSprite;

    public AudioClip seRock, seScissors, sePaper;
    public AudioSource aSE, bSE;

    private Player _aPlayer, _bPlayer;

    protected void Start()
    {
        _aPlayer = new Player(aRock, aScissors, aPaper, aPickupSukumi, aText);
        _bPlayer = new Player(bRock, bScissors, bPaper, bPickupSukumi, bText);

        _setHandler();
    }

    private void _setHandler()
    {
        aRock.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(aSE, seRock);
        });

        aScissors.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(aSE, seScissors);
        });

        aPaper.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(aSE, sePaper);
        });

        bRock.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(bSE, seRock);
        });

        bScissors.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(bSE, seScissors);
        });

        bPaper.handler = new RpsButtonOnClickHandler(button =>
        {
            _showPickupSukumi(button);
            _playSoundEffect(bSE, sePaper);
        });
    }

    private static void _showPickupSukumi(RpsButton button)
    {
        button.player.PickupSukumi = button.sukumi;
        button.player.PickupSukumiImage.sprite = button.SukumiIcon;
    }

    private static void _playSoundEffect(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.PlayOneShot(clip);
    }

    protected void Update()
    {
        foreach (var it in new[] {_aPlayer, _bPlayer})
        {
            if (!it.IsEnablePickupSukumi)
            {
                it.PickupSukumiImage.sprite = blankSprite;
            }
        }

        var player = Player.Battle(_aPlayer, _bPlayer);

        // ReSharper disable once InvertIf
        if (player != null)
        {
            player.message = Constants.WinningMessage;
            (player == _aPlayer ? _bPlayer : _aPlayer).message = Constants.LoseMessage;
        }
    }
}