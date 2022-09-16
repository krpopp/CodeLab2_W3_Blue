﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GameManagerScript : MonoBehaviour {

	public int gridWidth = 8;
	public int gridHeight = 8;
	public float tokenSize = 1;

	protected MatchManagerScript matchManager;
	protected InputManagerScript inputManager;
	protected RepopulateScript repopulateManager;
	protected MoveTokensScript moveTokenManager;

	public GameObject grid;
	public  GameObject[,] gridArray;
	protected Object[] tokenTypes;
	GameObject selected;

	public virtual void Start () {
		tokenTypes = (Object[])Resources.LoadAll("_Core/Tokens/");
		gridArray = new GameObject[gridWidth, gridHeight];
		MakeGrid();
		matchManager = GetComponent<MatchManagerScript>();
		inputManager = GetComponent<InputManagerScript>();
		//repopulateManager = GetComponent<RepopulateScript>();
		//moveTokenManager = GetComponent<MoveTokensScript>();
	}

	public virtual void Update(){
		if(!GridHasEmpty()){
			if(matchManager.GridHasMatch()){
				matchManager.RemoveMatches();
			} else {
				inputManager.SelectToken();
			}
		} else {
			//if(!moveTokenManager.move){
			//	moveTokenManager.SetupTokenMove();
			//}
			//if(!moveTokenManager.MoveTokensToFillEmptySpaces()){
			//	repopulateManager.AddNewTokensToRepopulateGrid();
			//}
		}
	}

	public string SayMyName(bool isOn)
	{
		return "stuff";
	}

	void MakeGrid() {
		grid = new GameObject("TokenGrid");
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight; y++){
				AddTokenToPosInGrid(x, y, grid);
			}
		}
	}

	public virtual bool GridHasEmpty(){
		for(int x = 0; x < gridWidth; x++){
			for(int y = 0; y < gridHeight ; y++){
				if(gridArray[x, y] == null){
					return true;
				}
			}
		}

		return false;
	}

	public Vector2 GetWorldPositionFromGridPosition(int x, int y){
		return new Vector2(
			(x - gridWidth/2) * tokenSize,
			(y - gridHeight/2) * tokenSize);
	}

	public void AddBigTokenToPosInGrid(int x, int y, GameObject parent){
		Vector3 position = GetWorldPositionFromGridPosition(x, y);
		GameObject token = 
			Instantiate(tokenTypes[Random.Range(0, tokenTypes.Length)], 
			            position, 
			            Quaternion.identity) as GameObject;
		token.transform.parent = parent.transform;
		gridArray[x, y] = token;
	}
}
