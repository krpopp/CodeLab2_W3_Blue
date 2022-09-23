using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMoveToken : MoveTokensScript
{
    public override void Update()
    {
        //override fix to set to normalised framerate across all devices
        if(move){
			lerpPercent += lerpSpeed * Time.deltaTime;

			if(lerpPercent >= 1){
				lerpPercent = 1;
			}

			if(exchangeToken1 != null){
				ExchangeTokens();
			}
		}
    }
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
					break; //override to fix token jumping? I took this from the class example script(KPMoveToken) but I'm unsure why this break is here
				}
			}
		}

		if (lerpPercent == 1)
		{
			move = false;
		}

		return movedToken;
	}
}
