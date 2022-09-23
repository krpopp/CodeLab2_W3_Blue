using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMatchManager : MatchManagerScript
{

    public AudioSource source;
    public AudioClip clip;

    //function to check for matches in the grid which has been extended to find vertical matches as well
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

    //function to find if there are vertical matches
    public bool GridHasVerticalMatch(int x, int y)
    {
        GameObject token1 = gameManager.gridArray[x, y + 0];
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

    //function to get the veritical match length to check if it is three or over
    public int GetVerticalMatchLength(int x, int y){
		int matchLength = 1;
		
		GameObject first = gameManager.gridArray[x, y];

		if(first != null){
			SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
			for(int i = y + 1; i < gameManager.gridWidth; i++){
				GameObject other = gameManager.gridArray[x, i];

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

    //Removes matches horizontally and vertically if it finds one. Doesn't remove both Horizontal and Vertical match at the same time. Only one of those if there are both horizontal and vertical match at the same time
    public override int RemoveMatches()
    {
        int numRemoved = 0;

		for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(x < gameManager.gridWidth - 2){

					int horizonMatchLength = GetHorizontalMatchLength(x, y);

					if(horizonMatchLength > 2){

						for(int i = x; i < x + horizonMatchLength; i++){
							GameObject token = gameManager.gridArray[i, y]; 
							Destroy(token);
                            source.PlayOneShot(clip); //plays Mario coin audio whenever a match is removed horizontally

							gameManager.gridArray[i, y] = null;
							numRemoved++;
						}
					}
				}
			}
		}

        for(int x = 0; x < gameManager.gridWidth; x++){
			for(int y = 0; y < gameManager.gridHeight ; y++){
				if(y < gameManager.gridWidth - 2){

					int verticalMatchLength = GetVerticalMatchLength(x, y);

					if(verticalMatchLength > 2){

						for(int i = y; i < y + verticalMatchLength; i++){
							GameObject token = gameManager.gridArray[x, i]; 
							Destroy(token);
                            source.PlayOneShot(clip); //plays Mario coin audio whenever a match is removed vertically
                            

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
