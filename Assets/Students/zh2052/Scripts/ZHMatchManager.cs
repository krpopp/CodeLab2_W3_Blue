using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHMatchManager : MatchManagerScript
{
	protected ZHBtnScript btnScript;

    public override void Start()
    {
        base.Start();
		btnScript = GameObject.Find("Bomb").GetComponent<ZHBtnScript>();
    }

    //protected GameManagerScript gameManager;

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

	public bool GridHasVerticalMatch(int x, int y)
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

	public int GetVerticalMatchLength(int x, int y)
    {
		int matchLength = 1;

		GameObject first = gameManager.gridArray[x, y];

		if (first != null)
		{
			SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();

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

	public override int RemoveMatches()
    {
		int numRemoved = 0;

		

		for (int x = 0; x < gameManager.gridWidth; x++)
		{
			for (int y = 0; y < gameManager.gridHeight; y++)
			{
				if (x < gameManager.gridWidth - 2)
				{

					int horizonMatchLength = GetHorizontalMatchLength(x, y);

					if (horizonMatchLength > 2)
					{

						if(horizonMatchLength > 3)
                        {
							btnScript.BombNum++;
                        }

						for (int i = x; i < x + horizonMatchLength; i++)
						{
							int verticalLengthUp = GetVerticalMatchLengthUp(i, y);
							int verticalLengthDown = GetVerticalMatchLengthDown(i, y);

							if (verticalLengthDown + verticalLengthUp > 2)
                            {
								for (int j = y + 1; j < y + verticalLengthDown; j++)
								{
									GameObject verticalToken = gameManager.gridArray[i, j];
									Destroy(verticalToken);

									gameManager.gridArray[i, j] = null;
									numRemoved++;
								}

								for (int j = y - 1; j > y - verticalLengthUp; j--)
								{
									GameObject verticalToken = gameManager.gridArray[i, j];
									Destroy(verticalToken);

									gameManager.gridArray[i, j] = null;
									numRemoved++;
								}
							}

							GameObject token = gameManager.gridArray[i, y];
							Destroy(token);

							gameManager.gridArray[i, y] = null;
							numRemoved++;
						}
					}
				}
			}
		}

		for (int x = 0; x < gameManager.gridWidth; x++)
		{
			for (int y = 0; y < gameManager.gridHeight; y++)
			{
				if (y < gameManager.gridHeight - 2)
				{
					// get vertical match length
					int verticalMatchLength = GetVerticalMatchLength(x, y);

					if (verticalMatchLength > 2)
					{
						for (int i = y; i < y + verticalMatchLength; i++)
						{

							GameObject token = gameManager.gridArray[x, i];
							Destroy(token);

							gameManager.gridArray[x, i] = null;
							numRemoved++;
						}

                        if (verticalMatchLength > 3)
                        {
							btnScript.BombNum++;
                        }
					}
				}
			}
		}

		return numRemoved;
    }

}
