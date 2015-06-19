using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollapseGridState : State
{
	private new Proxy proxy;
	private GridStack gridStack;

	public CollapseGridState(Proxy proxy) : base(proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initGridStackParsing();
	}

	public override void Exit()
	{
	
	}


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		gridStack = proxy.GameGridStack;
	}


	/** Init GridStack parsing. */
	private void initGridStackParsing()
	{
		parseGridStackDicitonary( gridStack.dictionary );
	}

	private void parseGridStackDicitonary(Dictionary<string, Grid> dictionary)
	{
		for( int i = 0; i < dictionary.Count; ++i )
		{
		    KeyValuePair<string, Grid> pair = dictionary.ElementAt( i );
		    Grid grid = pair.Value;

			parseGrid( grid );
		}
	}

	private void parseGrid(Grid grid)
	{
		grid.ForEveryObjectCall( delegate( int x, int y, object value )
		{
			// Debug.Log( x + " " + y + " " + value );

			GameObject positionValue = value as GameObject;
			GameObject compareValue = grid.Get( x, y + 1 ) as GameObject;

			if( compareValue == null )
			{

				Debug.Log( positionValue + " " + x + " " + y );
				
			}

		});

		// int numMovees = 0;

		// ArrayList listY = grid.

		// for( int y = 0; y < listY.Count; ++y )
		// {
		//     ArrayList listX = listY[ y ] as ArrayList;

		//     for( int x = 0; x < listX.Count; ++x )
		//     {
		//         GameObject gameObject = listX[ x ] as GameObject;
		//         GameObject compareObject = grid.Get( x, y - 1 );

		//         Debug.Log( "..." );
		//         Debug.Log( x + " " + y );
		//         Debug.Log( compareObject );
		//     }
		// }
	}
}