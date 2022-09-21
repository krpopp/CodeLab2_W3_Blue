using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LZGameManager : GameManagerScript
{
    public string[] colorNames = {"Blue", "Green", "Yellow", "Red", "purple"};

    public int deleteNumber = 0;
    public string colorName = "";

    public TextMeshProUGUI textNumber;
    public TextMeshProUGUI textColor;

    public override void Start()
    {
        base.Start();
        deleteNumber = Random.Range(1, 51);
        colorName = colorNames[Random.Range(0, colorNames.Length)];
    }

    public override void Update()
    {
        textNumber.text = deleteNumber.ToString();
        textColor.text = colorName;
        if (!GridHasEmpty())
        { //if grid is fully populated
            if (matchManager.GridHasMatch())
            { //if there are matches
                matchManager.RemoveMatches(); //remove the matches
                if(deleteNumber <= 0)
                {
                    deleteNumber = Random.Range(1, 100);
                    colorName = colorNames[Random.Range(0, colorNames.Length)];
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
