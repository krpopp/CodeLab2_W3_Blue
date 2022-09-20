using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHInputManager : InputManagerScript
{
	public int bombNum = 1;
	public bool bombClicked;
	protected ZHBtnScript btnScript;

    public override void Start()
    {
        base.Start();
		btnScript = GameObject.Find("Bomb").GetComponent<ZHBtnScript>();
    }

    private void Update()
    {
		bombClicked = btnScript.bombClicked;
		Debug.Log(bombClicked);
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

					if (bombClicked == true)
					{
						Debug.Log("Boom");

						selected = collider.gameObject;
						Vector2 pos = gameManager.GetPositionOfTokenInGrid(selected);

						for(int x = (int)(pos.x - 1); x <= (int)(pos.x + 1); x++)
                        {
							for (int y = (int)(pos.y - 1); y <= (int)(pos.y + 1); y++)
                            {
								if(x >= 0 && x < gameManager.gridWidth && y >= 0 && y < gameManager.gridHeight)
                                {
									GameObject token = gameManager.gridArray[x, y];
									Destroy(token);

									gameManager.gridArray[x, y] = null;

                                }
                            }
                        }

						selected = null;
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
