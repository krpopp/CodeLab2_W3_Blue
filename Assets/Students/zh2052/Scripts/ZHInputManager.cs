using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHInputManager : InputManagerScript
{
	protected ZHBtnScript btnScript;

	private bool bombClicked;

	public bool BombClicked
    {
		get { return bombClicked; }
		set { bombClicked = value; }
    }

    public override void Start()
    {
        base.Start();
		btnScript = GameObject.Find("Bomb").GetComponent<ZHBtnScript>();
    }

    public override void SelectToken()
    {
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Collider2D collider = Physics2D.OverlapPoint(mousePos);

			if (collider != null)
			{
				

				if (selected == null)
				{
					Debug.Log("selected");

					// if the bomb button is clicked, blow up the token surrounding the selected token
					if (bombClicked == true)
					{
						Debug.Log("Boom");

						// set the currently clicked token the selected
						selected = collider.gameObject;

						// get the position of the selected token
						Vector2 pos = gameManager.GetPositionOfTokenInGrid(selected);

						// blow up 3 * 3 tokens
						for(int x = (int)(pos.x - 1); x <= (int)(pos.x + 1); x++)
                        {
							for (int y = (int)(pos.y - 1); y <= (int)(pos.y + 1); y++)
                            {
								// see if the token is within the range of the grid
								if(x >= 0 && x < gameManager.gridWidth && y >= 0 && y < gameManager.gridHeight)
                                {

									// destroy the tokens and set them to 
									GameObject token = gameManager.gridArray[x, y];
									Destroy(token);

									gameManager.gridArray[x, y] = null;

                                }
                            }
                        }

						// 1 bomb is used
						btnScript.BombNum--;

						// unselect the token
						selected = null;

						// reset the bomb button
						bombClicked = false;
					}
                    else
                    {
						selected = collider.gameObject;
					}
				}
				else
				{
					Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
					Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

					if ((Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y)) == 1)
					{
						moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
					}
					selected = null;
				}
			}
		}
	}
}
