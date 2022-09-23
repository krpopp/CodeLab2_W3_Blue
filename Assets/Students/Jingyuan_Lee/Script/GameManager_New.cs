using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_New : GameManagerScript
{
    MatchManager_New matchManagerNew;

    public override void Start()
    {
        base.Start();
        matchManagerNew = GetComponent<MatchManager_New>(); 
    }

    public override void Update()
    {
        if (!GridHasEmpty())
        {
            if (matchManager.GridHasMatch())
            {
                matchManager.RemoveMatches();
                matchManagerNew.NulltheRing();

            }
            else
            {
                inputManager.SelectToken();
            }
        }
        else
        {
            if (!moveTokenManager.move)
            {
                moveTokenManager.SetupTokenMove();
            }
            if (!moveTokenManager.MoveTokensToFillEmptySpaces())
            {
                repopulateManager.AddNewTokensToRepopulateGrid();
            }
        }
    }
}
