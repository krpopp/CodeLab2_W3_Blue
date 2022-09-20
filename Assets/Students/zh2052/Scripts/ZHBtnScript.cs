using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZHBtnScript : MonoBehaviour
{
    private int bombNum = 1;
    public int BombNum
    {
        get { return bombNum; }
        set { bombNum = value; }
    }

    public Button bombButton;

    public Text text;

    protected ZHInputManager inputManager;

    
    // Start is called before the first frame update
    void Start()
    {
        bombButton = GetComponent<Button>();
        inputManager = GameObject.Find("GameManager").GetComponent<ZHInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "x" + bombNum;

        if (bombNum > 0)
        {
            bombButton.enabled = true;
        }
        else
        {
            bombButton.enabled = false;
        }
    }

    public void OnBombClicked()
    {
        inputManager.BombClicked = true;
        
    }
}
