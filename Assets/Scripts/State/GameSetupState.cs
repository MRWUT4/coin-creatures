using UnityEngine;

public class GameSetupState : State
{
	private new Proxy proxy;
	private State state;
	// private DoTween doTween;

	private Grid monsterGrid;
	private Grid coinGrid;
	private GameObject gameObject;


	public GameSetupState(Proxy proxy) : base(proxy)
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
		GameObject clone = (GameObject) GameObject.Instantiate( original, GetPosition(x, y), Quaternion.identity );
		clone.transform.parent = gameObject.transform;

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
		return GetGameObjectCloneAt( GetRandomMonster(), x, y );
	}


	/** Coin. */
	public GameObject GetCoinInstance(int x, int y)
	{
		return GetGameObjectCloneAt( proxy.Coin, x, y );
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
		// GridStack gridStack = new GridStack( 1, 1 );
		GridStack gridStack = new GridStack( proxy.Columns, proxy.Rows );
		proxy.GameGridStack = gridStack;

		monsterGrid = gridStack.AddGrid( Names.Monster );
		monsterGrid.ForEveryObjectCall( setupMonsterGridValues );

		Debug.Log( monsterGrid.ToTagNameString() );

		coinGrid = gridStack.AddGrid( Names.Coin );
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
		var coin = GetCoinInstance( x, y );
		coinGrid.Set( x, y, coin );

		// var interactionObject = ( InteractionObject ) coin.GetComponent< InteractionObject >();
		// interactionObject.OnMouseDown += coinOnMouseDownHandler;

		Mutate mutate = coin.GetComponent<Mutate>();
		mutate.alpha = .5f;
		// doTween.To( mutate, 1f, new { y = mutate.y + 10, alpha = 0, ease = "Back.EaseOut" } );
	}
}