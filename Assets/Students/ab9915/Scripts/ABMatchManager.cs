using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMatchManager : MatchManagerScript
{
    public override bool GridHasMatch()
    {
        bool hasAMatch = base.GridHasMatch();

        for(int x = 0; x < gameManager.gridWidth; x++)
        {
            for(int y = 0; y < gameManager.gridHeight - 2; y++)
            {
                hasAMatch = hasAMatch || GridHasVerticalMatch(x,y);
            }
        }
        return hasAMatch;
    }

    public bool GridHasVerticalMatch(int x, int y)
    {
        GameObject token1 = gameManager.gridArray[x, y];
		GameObject token2 = gameManager.gridArray[x, y + 1];
		GameObject token3 = gameManager.gridArray[x, y + 2];
		
		if(token1 != null && token2 != null && token3 != null){
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
			
			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
		} else {
			return false;
		}
    }

    public int GetVerticalMatchLength(int x, int y){
		int matchLength = 1;
		
		GameObject first = gameManager.gridArray[x, y];

		if(first != null){
			SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
			for(int i = x + 1; i < gameManager.gridWidth; i++){
				GameObject other = gameManager.gridArray[i, y];

				if(other != null){
					SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

					if(sr1.sprite == sr2.sprite){
						matchLength++;
					} else {
						break;
					}
				} else {
					break;
				}
			}
		}
		
		return matchLength;
	}

    public override int RemoveMatches()
    {
        int numRemoved = 0;

        for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(y < gameManager.gridWidth - 2){

					int verticalMatchLength = GetVerticalMatchLength(x, y);

					if(verticalMatchLength > 2){

						for(int i = x; i < x + verticalMatchLength; i++){
							GameObject token = gameManager.gridArray[x, i]; 
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
