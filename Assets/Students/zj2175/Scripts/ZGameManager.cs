using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZGameManager : GameManagerScript
{
    RectTransform rect;

    //called before the first frame update
    public override void Start()
    {
        base.Start();
        //got it to work but ultimately not needed
        //tokenTypes = (Object[])Resources.LoadAll("Prefabs/"); //grabbing prefabs

    }

}
