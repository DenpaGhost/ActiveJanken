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

    private Player _aPlayer, _bPlayer;

    // Start is called before the first frame update
    protected void Start()
    {
        _aPlayer = new Player(aRock, aScissors, aPaper, aPickupSukumi, aText);
        _bPlayer = new Player(bRock, bScissors, bPaper, bPickupSukumi, bText);

        setListener();
    }

    private void setListener()
    {
        var action = new RpsButtonOnClickHandler(button =>
        {
            button.player.PickupSukumi = button.sukumi;
            button.player.PickupSukumiImage.sprite = button.SukumiIcon;
        });

        var buttons = new[] {aRock, aScissors, aPaper, bRock, bScissors, bPaper};
        foreach (var rpsButton in buttons)
        {
            rpsButton.handler = action;
        }
    }

    // Update is called once per frame
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
        if (player == null) return;

        player.message = Constants.WinningMessage;
        (player == _aPlayer ? _bPlayer : _aPlayer).message = Constants.LoseMessage;
    }
}