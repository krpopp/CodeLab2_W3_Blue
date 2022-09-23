using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZHBtnScript : MonoBehaviour
{
    private int bombNum = 1;

    // the property of bomb number
    public int BombNum
    {
        get { return bombNum; }
        set { bombNum = value; }
    }

    public Button bombButton;

    public Text text;

    protected ZHInputManager inputManager;

    
    void Start()
    {
        // get the button that this class is attached to
        bombButton = GetComponent<Button>();
        inputManager = GameObject.Find("GameManager").GetComponent<ZHInputManager>();
    }

    void Update()
    {
        // the text on the bomb shows how many bomb the player have now
        text.text = "x" + bombNum;

        // if the player has some bombs, the bomb button can be clicked
        if (bombNum > 0)
        {
            bombButton.enabled = true;
        }
        else
        {
            bombButton.enabled = false;
        }
    }

    // whether the bomb button is clicked
    public void OnBombClicked()
    {
        inputManager.BombClicked = true;
        
    }
}
