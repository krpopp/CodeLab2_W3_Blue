using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_New : InputManagerScript
{
    [SerializeField]public GameObject ring;
    public Sprite selectRing;

    private MoveToken_New moveTokenNew;

    public override void Start()
    {
        //why the moveManager still point to the old MoveTokenScript
        //moveManager = GetComponent<MoveToken_New>();
        moveTokenNew = GetComponent<MoveToken_New>();
        gameManager = GetComponent<GameManagerScript>();       
    }

    public override void SelectToken()
    {

        // when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // get the mouse position inside the game world
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // get the collider from the mouse position
            Collider2D collider = Physics2D.OverlapPoint(mousePos);


            // check whether the token collider is clicked
            if (collider != null)
            {
                // check whether the token was clicked before
                if (selected == null)
                {
                    selected = collider.gameObject;

                    //select token
                    selectRing = Resources.Load<Sprite>("Sprites/Ring");
                    Vector3 selectPos = collider.transform.position;
                    ring.transform.position = selectPos;
                    ring.GetComponent<SpriteRenderer>().sprite = selectRing;
                }
                else
                {
                    // get the grid positions of the two tokens (one selected, one clicked)
                    Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                    Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                    // swap the two tokens' positions if they are close to each other

                    // DEBUG
                    float distance_x = Mathf.Abs(pos1.x - pos2.x);
                    float distance_y = Mathf.Abs(pos1.y - pos2.y);
                    if ((distance_x == 1 || distance_y == 1) && distance_x * distance_y == 0)
                    {
                        moveTokenNew.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                        //moveTokenNew.ExchangeRing();
                    }
                    // reset
                    selected = null;
                }
            }
        }

    }
}
