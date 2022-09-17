using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHMatchManager : MatchManagerScript
{
	public override bool GridHasMatch()
	{
		bool hasMatch = base.GridHasMatch();
		for (int x = 0; x < gameManager.gridWidth; x++)
		{
			for (int y = 0; y < gameManager.gridHeight; y++)
			{
				if (y < gameManager.gridHeight - 2)
				{
					hasMatch = hasMatch || GridHasVerticalMatch(x, y);
				}
			}
		}
		return hasMatch;
	}

	bool GridHasVerticalMatch(int x, int y)
    {
		// check vertical matches
		GameObject token1 = gameManager.gridArray[x, y + 0];
		GameObject token2 = gameManager.gridArray[x, y + 1];
		GameObject token3 = gameManager.gridArray[x, y + 2];

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
}
