using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZMatchManager : MatchManagerScript
{
    public bool falling;
    float speed = 16f;
    float gravity = 32f;
    Vector2 moveDir;
    RectTransform rect;

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
        GameObject token1 = gameManager.gridArray[x, y + 0];
		GameObject token2 = gameManager.gridArray[x, y + 1];
		GameObject token3 = gameManager.gridArray[x, y + 2];
		
		if(token1 != null && token2 != null && token3 != null)
		{
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
			
			//checks if sprites 1-3 are the same sprite, bool is true
			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
		} else 
		{
			return false;
		}

        //find if grid has vertical match
        //return true;
    }

    public int GetVerticalMatchLength(int x, int y)
	{
		//starts at 1, for a single token
		int matchLength = 1;
		
		//postion of first token
		GameObject first = gameManager.gridArray[x, y];

		//if first token is not null
		if(first != null)
		{
			//get the sprite of first token
			SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
			//checks the token after the first token !!!!!!!!!!!!!!bug?
			for(int i = y + 1; i < gameManager.gridWidth; i++)
			{
				GameObject other = gameManager.gridArray[x, i];

				if(other != null)
				{
					SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

					if(sr1.sprite == sr2.sprite)
					{
						matchLength++;
					} else 
					{
						break;
					}
				} else 
				{
					break;
				}
			}
		}

		return matchLength;
	}

    public override int RemoveMatches()
    {
        int numRemoved = base.RemoveMatches();

        for(int x = 0; x < gameManager.gridWidth; x++)
		{
			for(int y = 0; y < gameManager.gridHeight ; y++)
			{
				if(x < gameManager.gridWidth - 2)
				{
					//get the length of the Horizontal match
					int horizonMatchLength = GetHorizontalMatchLength(x, y);

					//if greater than 2
					if(horizonMatchLength > 2)
					{
						//destroy token game objects that are matching
						for(int i = x; i < x + horizonMatchLength; i++)
						{
							GameObject token = gameManager.gridArray[i, y]; 
							Destroy(token);

							//increases the number removed, makes sure that gameManager.gridArray sets
							//those spaces to empty
							gameManager.gridArray[i, y] = null;
							numRemoved++;
						}
					}
				}
				//NEW BUG FIX
				if(y < gameManager.gridHeight - 2)
				{
					//get the length of the Horizontal match
					int vertMatchLength = GetVerticalMatchLength(x, y);

					//if greater than 2
					if(vertMatchLength > 2)
					{
						//destroy token game objects that are matching
						for(int i = y; i < y + vertMatchLength; i++)
						{
							GameObject token = gameManager.gridArray[x, i]; 
							//Destroy(token);
                            Drop(token);

							//increases the number removed, makes sure that gameManager.gridArray sets
							//those spaces to empty
							gameManager.gridArray[x, i] = null;
							numRemoved++;
						}
					}
				}
			}
		}
        
        return numRemoved;
    }

    public void Drop(GameObject token)
    {
        falling = true;

        moveDir = Vector2.up;
        moveDir.x = Random.Range(-1f, 1f);
        moveDir *= speed / 2;

        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(!falling)
        {
            return;
        }

        moveDir.y -= Time.deltaTime * gravity;
        moveDir.x = Mathf.Lerp(moveDir.x, 0, Time.deltaTime);
        rect.anchoredPosition += moveDir * Time.deltaTime * speed;

        if(rect.position.x < -32f || rect.position.x > Screen.width + 32f || 
        rect.position.y < -32f || rect.position.y > Screen.height + 32f)
        {
            falling = false;
        }
    }
}
