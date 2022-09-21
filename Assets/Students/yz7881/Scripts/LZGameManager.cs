using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LZGameManager : GameManagerScript
{
    public string[] colorNames = { "Blue", "Green", "Yellow", "Red", "purple" };
    private Color[] colorlist = { Color.cyan, Color.green, Color.yellow, Color.red, Color.magenta };
    private int colorNumber = 0;
    private const float gameTime = 60;

    public int deleteNumber = 0;
    public string colorName = "";
    public float timeLeft;

    public TextMeshProUGUI textNumber;
    public TextMeshProUGUI textColor;
    public TextMeshProUGUI textTime;

    public override void Start()
    {
        base.Start();
        deleteNumber = Random.Range(1, 51);
        colorNumber = Random.Range(0, colorNames.Length);
        colorName = colorNames[colorNumber];
        timeLeft = gameTime;
    }

    private void updateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        } else
        {
            timeLeft = gameTime;
            resetColorAndNumber();
            
        }

        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);

        textTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void resetColorAndNumber()
    {
        deleteNumber = Random.Range(1, 100);
        colorNumber = Random.Range(0, colorNames.Length);
        colorName = colorNames[colorNumber];
    }

    public override void Update()
    {
        updateTimer();
        textNumber.text = deleteNumber.ToString();
        textColor.text = colorName;
        textColor.color = colorlist[colorNumber];
        if (!GridHasEmpty())
        { //if grid is fully populated
            if (matchManager.GridHasMatch())
            { //if there are matches
                matchManager.RemoveMatches(); //remove the matches
                if(deleteNumber <= 0)
                {
                    resetColorAndNumber();
                    timeLeft = gameTime;
                }
            }
            else
            {
                inputManager.SelectToken(); //allow token to be selected
            }
        }
        else
        { //if grid not fully populated
            if (!moveTokenManager.move)
            { //if token movement is false
                moveTokenManager.SetupTokenMove(); //set it true so they can fill empty space
            }
            if (!moveTokenManager.MoveTokensToFillEmptySpaces())
            { //if is false
                repopulateManager.AddNewTokensToRepopulateGrid(); //and there are still free spaces, allow it to populate
            }
        }
    }
}
