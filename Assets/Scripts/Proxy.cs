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
	public Settings Settings = new Settings();

	[HideInInspector]
	public GameObject GameStateGameObject;

	[HideInInspector]
	public List< Dictionary<string, object> > IntersectionList;

	[HideInInspector]
	public TweenFactory TweenFactory;


	public Proxy()
	{
		TweenFactory = new TweenFactory( this.Settings.Duration, this.Settings.Delay );
	}
}

public class Settings
{
	public float Duration = .6f;
	public float Delay = .1f;
	public float OpenTimeout = 1f;
}

public class Names
{
	public const string Monster = "Monster";
	public const string Coin = "Coin";
	public const string Remove = "Remove";

	/** animation lables. */
	public const string AnimationMute = "Mute";
	public const string AnimationIdle = "Idle";
	public const string AnimationCoinSpinOut = "CoinSpinOut";
	public const string AnimationCoinSpinIn = "CoinSpinIn";

	/** game states. */
	public const string GameSetupState = "GameSetupState";
	public const string CoinSelectState = "CoinSelectState";
	public const string RemoveMonsterState = "RemoveMonsterState";
	public const string ClearGridValuesState = "ClearGridValuesState";
	public const string CollapseGridState = "CollapseGridState";
}