using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToken_New : MoveTokensScript
{
    InputManager_New inputManagerNew;

    public override void Start()
    {
        gameManager = GetComponent<GameManagerScript>();
        matchManager = GetComponent<MatchManagerScript>();
        inputManagerNew = GetComponent<InputManager_New>();

        lerpPercent = 0;
    }

    public override void Update()
    {

        if (move)
        {
            lerpPercent += lerpSpeed;

            if (lerpPercent >= 1)
            {
                lerpPercent = 1;
            }

            if (exchangeToken1 != null)
            {
                ExchangeTokens();
                ExchangeRing();
            }
        }
    }

    public override void ExchangeTokens()
    {
        base.ExchangeTokens();
    }

    public void ExchangeRing()
    {
        if(exchangeToken1 != null)
            inputManagerNew.ring.transform.position = exchangeToken1.transform.position;
    }


    private Vector3 SmoothLerp(Vector3 startPos, Vector3 endPos, float lerpPercent)
    {
        return new Vector3(
            Mathf.SmoothStep(startPos.x, endPos.x, lerpPercent),
            Mathf.SmoothStep(startPos.y, endPos.y, lerpPercent),
            Mathf.SmoothStep(startPos.z, endPos.z, lerpPercent));
    }

    // move a token to a single empty block
    public override void MoveTokenToEmptyPos(int startGridX, int startGridY,
                                    int endGridX, int endGridY,
                                    GameObject token)
    {

        // get the positions of start and end
        Vector3 startPos = gameManager.GetWorldPositionFromGridPosition(startGridX, startGridY);
        Vector3 endPos = gameManager.GetWorldPositionFromGridPosition(endGridX, endGridY);

        // get the position bewteen start and end positions
        Vector3 pos = Vector3.Lerp(startPos, endPos, lerpPercent);

        // move the token to the position
        token.transform.position = pos;

        // once the token is moved to the end pos, change it in the grid array
        if (lerpPercent == 1)
        {
            gameManager.gridArray[endGridX, endGridY] = token;
            gameManager.gridArray[startGridX, startGridY] = null;
        }
    }

    // move tokens to all empty spaces and return the bool value whether any tokens are moved
    public override bool MoveTokensToFillEmptySpaces()
    {
        bool movedToken = false;

        for (int x = 0; x < gameManager.gridWidth; x++)
        {
            for (int y = 1; y < gameManager.gridHeight; y++)
            {
                if (gameManager.gridArray[x, y - 1] == null)
                {
                    for (int pos = y; pos < gameManager.gridHeight; pos++)
                    {
                        GameObject token = gameManager.gridArray[x, pos];
                        if (token != null)
                        {
                            MoveTokenToEmptyPos(x, pos, x, pos - 1, token);
                            movedToken = true;
                        }
                    }
                }
            }
        }

        // once the move is finished, disable move by resetting the value to false
        if (lerpPercent == 1)
        {
            move = false;
        }

        return movedToken;
    }


}
