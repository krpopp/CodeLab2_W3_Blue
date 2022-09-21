using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchManager_New : MatchManagerScript
{
    InputManager_New inputManagerNew;
    public int score = 0;
    [SerializeField] int eachTokenScore = 10;

    [SerializeField] TMP_Text scoreText;

    public override void Start()
    {
        base.Start();
        inputManagerNew = GetComponent<InputManager_New>();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
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

    public override bool GridHasMatch()
    {
        bool match = false;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {
                if (x < gameManager.gridWidth - 2)
                {
                    match = match || GridHasHorizontalMatch(x, y);
                }

                if (y < gameManager.gridHeight - 2)
                {
                    match = match || GridHasVerticalMatch(x, y);
                }
            }
        }

        return match;
    }

    public int GetVerticalMatchLengthDown(int x, int y)
    {
        int matchDownLength = 1;

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
                        matchDownLength++;
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
        return matchDownLength;

    }

    public int GetVerticalMatchLengthUp(int x, int y)
    {
        int matchUpLength = 1;

        // get the first token
        GameObject first = gameManager.gridArray[x, y];

        if (first != null)
        {

            // get the sprite of the first token
            SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

            // find how many tokens have the same sprite as the first token's on the same horizontal line
            for (int i = y - 1; i > 0; i--)
            {
                GameObject other = gameManager.gridArray[x, i];

                if (other != null)
                {
                    SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

                    if (sr1.sprite == sr2.sprite)
                    {
                        matchUpLength++;
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
        return matchUpLength;

    }

    // remove all the matched tokens and return the number of removed tokens
    public override int RemoveMatches()
    {
        int numRemoved = 0;

        // go through the entire grid
        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 0; y < gameManager.gridHeight; y++)
            {

                if (x < gameManager.gridWidth - 2)
                {

                    // find how many tokens match on the horizontal line
                    int horizonMatchLength = GetHorizontalMatchLength(x, y);

                    // remove all the matched tokens if the number of matched token is larger than 2
                    if (horizonMatchLength > 2)
                    {
                        score += eachTokenScore*horizonMatchLength;

                        for (int i = x; i < x + horizonMatchLength; i++)
                        {
                            GameObject token = gameManager.gridArray[i, y];

                            int verticalMatchLengthDown = GetVerticalMatchLengthDown(i, y);
                            int verticalMatchLengthUp = GetVerticalMatchLengthUp(i, y);

                            //Add cross match logic
                            if (verticalMatchLengthDown + verticalMatchLengthUp > 3)
                            {
                                score += eachTokenScore * (verticalMatchLengthDown + verticalMatchLengthUp-1);

                                for (int j = y + 1; j < y + verticalMatchLengthDown; j++)
                                {
                                    GameObject verticalToken = gameManager.gridArray[i, j];
                                    Destroy(verticalToken);

                                    gameManager.gridArray[i, j] = null;
                                    numRemoved++;
                                }

                                for (int j = y - 1; j > y - verticalMatchLengthUp; j--)
                                {
                                    GameObject verticalToken = gameManager.gridArray[i, j];
                                    Destroy(verticalToken);

                                    gameManager.gridArray[i, j] = null;
                                    numRemoved++;
                                }
                            }

                            Destroy(token);

                            gameManager.gridArray[i, y] = null;
                            numRemoved++;
                        }
                    }
                }


                if (y < gameManager.gridHeight - 2)
                {
                    int verticalMatchLength = GetVerticalMatchLengthDown(x, y);
                    if (verticalMatchLength > 2)
                    {
                        score += eachTokenScore * verticalMatchLength;

                        for (int j = y; j < y + verticalMatchLength; j++)
                        {
                            GameObject token = gameManager.gridArray[x, j];
                            Destroy(token);

                            gameManager.gridArray[x, j] = null;
                            numRemoved++;
                        }
                    }
                }

            }
        }

        return numRemoved;
    }

    public void NulltheRing()
    {
        inputManagerNew.ring.GetComponent<SpriteRenderer>().sprite = null;
    }

}

