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

        tokenTypes = (Object[])Resources.LoadAll("Prefabs/"); //grabbing prefabs
    }

}
