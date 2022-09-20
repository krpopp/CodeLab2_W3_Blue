using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LZInputManager : InputManagerScript
{
    // detect which token is clicked
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
                // check whether the token was clicked before???
                if (selected == null)
                {
                    selected = collider.gameObject;
                }
                else
                {
                    // get the positions of the two tokens (one selected, one clicked)
                    Vector2 pos1 = gameManager.GetPositionOfTokenInGrid(selected);
                    Vector2 pos2 = gameManager.GetPositionOfTokenInGrid(collider.gameObject);

                    // swap the two tokens' positions if they are close to each other

                    // DEBUG -- disable diagonal token exchange
                    if ((Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y)) == 1)
                    {
                        moveManager.SetupTokenExchange(selected, pos1, collider.gameObject, pos2, true);
                    }
                    // reset
                    selected = null;
                }
            }
        }

    }
}
