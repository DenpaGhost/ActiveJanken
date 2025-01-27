﻿using System;
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

    public AudioClip seRock, seScissors, sePaper, startSE, finishSE;
    public AudioSource aSE, bSE, systemSE;

    public Title title;
    public ResultText aResult, bResult;

    private Player _aPlayer, _bPlayer;
    private GameState _state = GameState.Idle;


    public GameState State
    {
        get => _state;
        set
        {
            _state = value;

            switch (_state)
            {
                case GameState.Idle:
                    OnIdle();
                    break;
                case GameState.Battle:
                    OnBattle();
                    break;
                case GameState.Result:
                    OnResult();
                    break;
                case GameState.ResultLock:
                    OnResultLock();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    protected void Start()
    {
        _aPlayer = new Player(aRock, aScissors, aPaper, aPickupSukumi, aText, aResult);
        _bPlayer = new Player(bRock, bScissors, bPaper, bPickupSukumi, bText, bResult);

        _setHandler();
    }

    private void _setHandler()
    {
        aRock.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(aSE, seRock);
        });

        aScissors.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(aSE, seScissors);
        });

        aPaper.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(aSE, sePaper);
        });

        bRock.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(bSE, seRock);
        });

        bScissors.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(bSE, seScissors);
        });

        bPaper.handler = new RpsButtonOnClickHandler(button =>
        {
            _buttonClickEvent(button);
            _playSoundEffect(bSE, sePaper);
        });
    }

    private void _buttonClickEvent(RpsButton button)
    {
        switch (State)
        {
            case GameState.Idle:
                if (button.player.State == PlayerState.Idle)
                {
                    button.player.State = PlayerState.Ready;
                }

                break;
            case GameState.Battle:
                button.player.PickupSukumi = button.sukumi;
                button.player.PickupSukumiImage.sprite = button.SukumiIcon;
                break;
            case GameState.Result:
                State = GameState.Idle;
                break;
            case GameState.ResultLock:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void _playSoundEffect(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.PlayOneShot(clip);
    }

    protected void Update()
    {
        if (State == GameState.Battle)
        {
            foreach (var it in new[] {_aPlayer, _bPlayer})
            {
                if (!it.IsEnablePickupSukumi)
                {
                    it.PickupSukumiImage.sprite = blankSprite;
                }
            }

            var player = Player.Battle(_aPlayer, _bPlayer);
            if (player != null)
            {
                player.ResultText.Message = Constants.WinningMessage;
                (player == _aPlayer ? _bPlayer : _aPlayer).ResultText.Message = Constants.LoseMessage;

                State = GameState.ResultLock;
            }
        }

        if (State == GameState.Idle && _aPlayer.State == PlayerState.Ready && _bPlayer.State == PlayerState.Ready)
        {
            State = GameState.Battle;
        }
    }

    private void OnIdle()
    {
        title.Show();
        _aPlayer.Message = _bPlayer.Message = Constants.WaitingMessage;

        _aPlayer.PickupSukumiImage.sprite = blankSprite;
        _bPlayer.PickupSukumiImage.sprite = blankSprite;

        _aPlayer.ResultText.Close();
        _bPlayer.ResultText.Close();
    }

    private void OnBattle()
    {
        title.Close();
        systemSE.Stop();
        systemSE.PlayOneShot(startSE);

        _aPlayer.Message = "";
        _bPlayer.Message = "";
    }

    private void OnResultLock()
    {
        systemSE.Stop();
        systemSE.PlayOneShot(finishSE);

        _aPlayer.ResultText.Show();
        _bPlayer.ResultText.Show();

        StartCoroutine(waitForResult());
    }

    private IEnumerator waitForResult()
    {
        yield return new WaitForSeconds(Constants.WaitForResultTime);
        State = GameState.Result;
    }

    private void OnResult()
    {
        _aPlayer.Message = _bPlayer.Message = Constants.TapToReturn;

        _aPlayer.State = PlayerState.Idle;
        _bPlayer.State = PlayerState.Idle;
    }
}