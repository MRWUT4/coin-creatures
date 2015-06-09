using UnityEngine;

public class GameSetupComponent : MonoBehaviour
{
	private State state;
	private Proxy proxy;

	private Grid monsterGrid;
	private Grid coinGrid;


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
		GameObject clone = (GameObject) Instantiate( original, GetPosition(x, y), Quaternion.identity );
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
	 * Component interface.
	 */

	public void Start()
	{
		initVariables();
		initGridStack();
		initStateExit();
	}


	/**
	 * Private interface.
	 */

	/** Create Module Variables. */
	private void initVariables()
	{
		state = gameObject.GetComponent<StateInfo>().state;
		proxy = state.proxy as Proxy;
	}


	/** GridStack functions. */
	private void initGridStack()
	{
		GridStack gridStack = new GridStack( proxy.Columns, proxy.Rows );
		proxy.GameGridStack = gridStack;

		monsterGrid = gridStack.AddGrid( Names.Monster );
		monsterGrid.ForEveryObjectCall( setupMonsterGridValues );

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
	}


	/** Exit State. */
	private void initStateExit()
	{
		
	}
}