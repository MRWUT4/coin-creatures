using System.Collections;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour 
{	
	public static string ID_STATE_COIN_SELECT = "coinSelect";

	public int Columns = 6;
	public int Rows = 8;
	public int Distance = 1;

	public GameObject[] MonsterList;
	public GameObject Coin;

	private GameObject holder;
	private Grid monsterGrid;
	private Grid coinGrid;
	private StateMachine stateMachine;


	/**
	 * Public interface.
	 */

	void Start () 
	{
		initVariables();
		initGridStack();
		iniStateMachine();
	}

	void Update () 
	{
		stateMachine.Update();
	}


	/**
	 * Getter / Setter
	 */

	public Vector3 GetPosition(int x, int y)
	{
		return new Vector3( x * Distance, y * Distance );
	}

	public GameObject GetRandomMonster()
	{
		return MonsterList[ UnityEngine.Random.Range( 0, MonsterList.Length ) ];
	}

	public GameObject GetRandomMonsterInstance(int x, int y)
	{
		var original = GetRandomMonster();
		var gameObject = (GameObject) Instantiate( original, GetPosition(x, y), Quaternion.identity );

		gameObject.transform.parent = holder.transform;

		return gameObject;
	}

	public GameObject GetCoInstance(int x, int y)
	{
		var gameObject = (GameObject) Instantiate( Coin, GetPosition(x, y), Quaternion.identity );
		gameObject.transform.parent = holder.transform;

		return gameObject;
	}


	/**
	 * Private interface. 
	 */

	private void initVariables()
	{
		holder = new GameObject( "Holder" );
		holder.transform.parent = gameObject.transform;
	}
	

	/** GridStack functions. */
	private void initGridStack()
	{
		var gridStack = new GridStack( Columns, Rows );

		monsterGrid = gridStack.AddGrid( "monster" );
		monsterGrid.ForEveryObjectCall( setupMonsterGridValues );

		coinGrid = gridStack.AddGrid( "coin" );
		coinGrid.ForEveryObjectCall( setupCoinGridValues );
	}

	private void setupMonsterGridValues(int x, int y, object value)
	{
		var monster = GetRandomMonsterInstance( x, y );
		monsterGrid.Set( x, y, monster ); 
	}

	/** Setup Coins */
	private void setupCoinGridValues(int x, int y, object value)
	{
		var coin = GetCoInstance( x, y );

		coinGrid.Set( x, y, coin );

		var interactionObject = ( InteractionObject ) coin.GetComponent< InteractionObject >();
		interactionObject.OnMouseDown += coinOnMouseDownHandler;
	}

	private void coinOnMouseDownHandler(MonoBehaviour interactionObject)
	{
		Debug.Log( "coinOnMouseDownHandler", interactionObject );
	}


	/** StateMachine functions. */
	private void iniStateMachine()
	{
		Proxy proxy = new Proxy();

		proxy.gameObject = GetRandomMonsterInstance( 1, 0 );

		stateMachine = new StateMachine();
		stateMachine.OnExit += stateMachineOnExitHandler;
		stateMachine.OnExit += stateMachineOnEnterHandler;

		stateMachine.AddState( ID_STATE_COIN_SELECT, new CoinSelectState( proxy ) );
		stateMachine.SetState( ID_STATE_COIN_SELECT );
	}

	private void stateMachineOnExitHandler(State state)
	{
		Debug.Log( state.id );
	}

	private void stateMachineOnEnterHandler(State state)
	{
		Debug.Log( state.id );
	}
}