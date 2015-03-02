using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour 
{	
	public int Columns = 6;
	public int Rows = 8;
	public int Distance = 1;

	public GameObject[] MonsterList;
	public GameObject Coin;

	private GameObject holder;
	private Grid monsterGrid;
	private Grid coinGrid;

	/*
	 * Public interface.
	 */

	void Start () 
	{
		initVariables();
		initGridStack();
	}

	void Update () 
	{
	
	}


	/*
	 * Getter / Setter
	 */

	public Vector3 GetPosition(int x, int y)
	{
		return new Vector3( x * Distance, y * Distance );
	}

	public GameObject GetRandomMonster()
	{
		return MonsterList[ Random.Range( 0, MonsterList.Length ) ];
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


	/*
	 * Private interface. 
	 */

	private void initVariables()
	{
		holder = new GameObject( "Holder" );
		holder.transform.parent = gameObject.transform;
	}
	
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

		if( interactionObject )
			interactionObject.OnMouseDown = coinOnMouseDownHandler;
	}

	private void coinOnMouseDownHandler(InteractionObject interactionObject)
	{
		var position = interactionObject.transform.position;
		Debug.Log( position.x );
	}
}