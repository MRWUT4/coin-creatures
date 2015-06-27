using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AddGridValueState : State
{
	private new Proxy proxy;
	private State state;
	// private DoTween doTween;

	private GameObject gameObject;
	private delegate GameObject GetGameObjectDelegate(int x, int y);


	public AddGridValueState(Proxy proxy) : base(proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Getter / Setter
	 */

	/** GameObject. */
	public Vector3 GetPosition(int x, int y)
	{
		return new Vector3( x * proxy.Distance, y * proxy.Distance );
	}

	public GameObject GetGameObjectCloneAt(GameObject original, int x, int y)
	{
		GameObject clone = (GameObject) GameObject.Instantiate( original, GetPosition(x, y + 1), Quaternion.identity );
		clone.transform.parent = gameObject.transform;

		// Mutate mutate = gameObject.GetComponent<Mutate>();
		// mutate.alpha = .5f;

		return clone;
	}


	/** Monster. */
	public GameObject GetRandomMonster()
	{
		GameObject[] monsterList = proxy.MonsterList;
		return monsterList[ UnityEngine.Random.Range( 0, monsterList.Length ) ];
	}

	public GameObject GetRandomMonsterInstance(int x, int y)
	{
		GameObject gameObject = GetGameObjectCloneAt( GetRandomMonster(), x, y );
		// gameObject.GetComponent<Renderer>().enabled = false;
		return gameObject;
	}


	/** Coin. */
	public GameObject GetCoinInstance(int x, int y)
	{
		GameObject gameObject = GetGameObjectCloneAt( proxy.Coin, x, y );
		return gameObject;
	}


	/** GridStack. */
	public GridStack gridStack
	{		
		get 
	    { 
			if( proxy.GameGridStack == null )
				proxy.GameGridStack = new GridStack( proxy.Columns, proxy.Rows );	

	        return proxy.GameGridStack; 
	    }
	}

	public Grid monsterGrid
	{
		get
		{
			Grid grid = gridStack.GetGrid( Names.Monster );

			if( grid == null )
				grid = gridStack.AddGrid( Names.Monster );

			return grid;
		}
	}

	public Grid coinGrid
	{
		get
		{
			Grid grid = gridStack.GetGrid( Names.Coin );

			if( grid == null )
				grid = gridStack.AddGrid( Names.Coin );

			return grid;
		}
	}


	/**
	 * Override interface.
	 */

	public override void Enter()
	{
		initVariables();
		initGridStack();

		InvokeExit();
	}

	// public override void FixedUpdate()
	// {
		// doTween.Update();
	// }


	/**
	 * Private interface.
	 */

	/** Create Module Variables. */
	private void initVariables()
	{
		gameObject = proxy.GameStateGameObject;
		// doTween = new DoTween();
		//state = gameObject.GetComponent<StateInfo>().state;
	}


	/** GridStack functions. */
	private void initGridStack()
	{
		int amount = monsterGrid.NumValues == 0 ? proxy.BeginRows : proxy.AddRows;

		addGameObjectToTopRows( GetRandomMonsterInstance, monsterGrid, amount );
		addGameObjectToTopRows( GetCoinInstance, coinGrid, amount );

		// monsterGrid.ForEveryObjectCall( setupMonsterGridValues );
		// coinGrid.ForEveryObjectCall( setupCoinGridValues );
	}

	private void addGameObjectToTopRows(GetGameObjectDelegate getGameObject, Grid grid, int amount)
	{
		ArrayList listY = grid.listY;
		int gridWith = grid.width;

		for( int x = 0; x < gridWith; ++x )
		{
			int numSet = 0;

		    for( int y = 0; y < listY.Count; ++y )
		    {
		        object value = ( listY[ y ] as ArrayList )[ x ];
		        
		        if( value == null )
		        {
		        	numSet++;
		        	
		        	GameObject gameObject = getGameObject( x , y );
		        	grid.Set( x, y, gameObject );

				    if( numSet >= amount )
				    	break;
				}
		    }
		}
	}

	// private void setupMonsterGridValues(int x, int y, object value)
	// {
	// 	var monster = GetRandomMonsterInstance( x, y );
	// 	monsterGrid.Set( x, y, monster ); 
	// }


	// /** Setup Coins */
	// private void setupCoinGridValues(int x, int y, object value)
	// {
	// 	var coin = GetCoinInstance( x, y );
	// 	coinGrid.Set( x, y, coin );

	// 	// var interactionObject = ( InteractionObject ) coin.GetComponent< InteractionObject >();
	// 	// interactionObject.OnMouseDown += coinOnMouseDownHandler;

	// 	Mutate mutate = coin.GetComponent<Mutate>();
	// 	mutate.alpha = .5f;
	// 	// doTween.To( mutate, 1f, new { y = mutate.y + 10, alpha = 0, ease = "Back.EaseOut" } );
	// }
}