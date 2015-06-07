using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Proxy : GameObjectProxy
{
	public int Columns = 6;
	public int Rows = 8;
	public int Distance = 4;

	public GameObject[] MonsterList;
	public GameObject Coin;


	public GridStack GameGridStack;

	public Names Names = new Names();

}

public class Names
{
	public string Monster = "monster";
	public string Coin = "coin";

	/** game states. */
	public string GameSetup = "gameSetup";
}