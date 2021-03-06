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
		InvokeExit();
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
		int numMoves = 0;

		grid.ForEveryObjectCall( delegate( int x, int y, object value )
		{
			GameObject positionValue = value as GameObject;
			GameObject compareValue = grid.Get( x, y + 1 ) as GameObject;

			if( positionValue == null && compareValue != null )
			{			
				grid.Set( x, y + 1, null );
				grid.Set( x, y, compareValue );

				numMoves++;
			}
		});

		// Debug.Log( numMoves + " " + grid.ToTagNameString() );

		if( numMoves > 0 )
			parseGrid( grid );
	}
}