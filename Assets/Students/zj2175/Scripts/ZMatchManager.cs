using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMatchManager : MatchManagerScript
{
    public override bool GridHasMatch()
    {
        bool hasMatch = base.GridHasMatch();
        for(int x = 0; x < gameManager.gridWidth; x++)
        {
            for(int y = 0; y < gameManager.gridHeight - 2; y++)
            {
                hasMatch = hasMatch || GridHasVerticalMatch(x, y);
            }
        }

        return hasMatch;
    }

    public bool GridHasVerticalMatch(int x, int y)
    {
        //find if grid has vertical match
        return true;
    }

    public override int RemoveMatches()
    {
        int numRemoved = base.RemoveMatches();
        
        return numRemoved;
    }
}
