using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZHBtnScript : MonoBehaviour
{
    public bool bombClicked = false;
    public int bombNum = 1;
    public Button bombButton;

    
    // Start is called before the first frame update
    void Start()
    {
        bombButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
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
        bombClicked = true;
        
    }
}
