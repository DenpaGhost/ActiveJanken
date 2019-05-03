using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Models;
using UnityEngine;

public class SceneModel : MonoBehaviour
{
    public RpsButton
        aRock, aScissors, aPaper, bRock, bScissors, bPaper;

    private Player _aPlayer, _bPlayer;

    // Start is called before the first frame update
    protected void Start()
    {
        _aPlayer = new Player();
        aRock.player = _aPlayer;
        aScissors.player = _aPlayer;
        aPaper.player = _aPlayer;
        
        setListener();

    }
    
    private void setListener()
    {
        aRock.listener = 
        
    }

    // Update is called once per frame
    protected void Update()
    {
    }
}