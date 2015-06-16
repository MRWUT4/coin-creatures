using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ClearGridValuesState : State
{
	private new Proxy proxy;
	private GridStack gridStack;
	private List< Dictionary<string, object> > intersectionList;


	public ClearGridValuesState(Proxy proxy) : base(proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initIntersectionListParsing();
		
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
		intersectionList = proxy.IntersectionList;
		gridStack = proxy.GameGridStack;
	}


	/** Init intersectionListParsing. */
	private void initIntersectionListParsing()
	{
		if( intersectionList != null )
		{
			// ignore last (wrong) element 
			for( int i = 0; i < intersectionList.Count - 1; ++i )
			{
			    Dictionary<string, object> dictionary = intersectionList[ i ];

			    for( int j = 0; j < dictionary.Count; ++j )
			    {
			        KeyValuePair<string, object> pair = dictionary.ElementAt( j );
			     	Grid grid = gridStack.GetGrid( pair.Key );
			     	Point point = grid.GetPositionOfObject( pair.Value );

			     	grid.Set( point, null );
			     	
			     	// Debug.Log( grid.ToString() );
			    }
			}
		}
	}
}