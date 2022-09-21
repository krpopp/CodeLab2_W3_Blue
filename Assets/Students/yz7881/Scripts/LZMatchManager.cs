using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LZMatchManager : MatchManagerScript
{

    public override bool GridHasMatch()
    {
        bool hasMatch = base.GridHasMatch();

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight - 2; y++)
            {
                //if(y < gameManager.gridHeight - 2)
                //{
                hasMatch = hasMatch || GridHasVerticalMatch(x, y);
                //}
            }
        }

        return hasMatch;
    }

    public bool GridHasVerticalMatch(int x, int y)
    {
        // get 3 tokens in a column
        GameObject token1 = gameManager.gridArray[x, y + 0];
        GameObject token2 = gameManager.gridArray[x, y + 1];
        GameObject token3 = gameManager.gridArray[x, y + 2];

        // check whether all 3 tokens have the same sprite (color)
        if (token1 != null && token2 != null && token3 != null)
        {
            SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
            SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
            SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();

            return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
        }
        else
        {
            return false;
        }
    }


    public int GetVerticalMatchLength(int x, int y)
    {
        int matchLength = 1;

        // get the first token
        GameObject first = gameManager.gridArray[x, y];

        if (first != null)
        {

            // get the sprite of the first token
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

            // find how many tokens have the same sprite as the first token's on the same horizontal line
            for (int i = y + 1; i < gameManager.gridHeight; i++)
            {
                GameObject other = gameManager.gridArray[x, i];

                if (other != null)
                {
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                    if (sr1.sprite == sr2.sprite)
                    {
                        matchLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return matchLength;

    }

    // remove the vertical and horizontal tokens
    public override int RemoveMatches()
    {
        int numRemoved = 0;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (x < gameManager.gridWidth - 2)
                {
                    int horizontalMatchLength = GetHorizontalMatchLength(x, y);

                    if (horizontalMatchLength > 2)
                    {
                        for (int i = x; i < x + horizontalMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[i, y];
                            if (token.GetComponent<SpriteRenderer>().sprite.name == GetComponent<LZGameManager>().colorName)
                            {
                                GetComponent<LZGameManager>().deleteNumber -= 1;
                            }
                            Destroy(token);

                            gameManager.gridArray[i, y] = null;
                            numRemoved++;
                        }
                    }
                }
                if (y < gameManager.gridHeight - 2)
                {
                    int verticalMatchLength = GetVerticalMatchLength(x, y);

                    if (verticalMatchLength > 2)
                    {

                        for (int i = y; i < y + verticalMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[x, i];
                            if (token.GetComponent<SpriteRenderer>().sprite.name == GetComponent<LZGameManager>().colorName)
                            {
                                GetComponent<LZGameManager>().deleteNumber -= 1;
                            }
                            Destroy(token);

                            gameManager.gridArray[x, i] = null;
                            numRemoved++;
                        }
                    }
                }
            }
        }

        return numRemoved;
    }
}
