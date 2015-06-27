using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveItemsState : State
{
	private new Proxy proxy;
	private DoTween doTween;
	private GridStack gridStack;
	private TweenFactory tweenFactory;

	public MoveItemsState(Proxy proxy) : base(proxy)
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

	public override void FixedUpdate()
	{
		doTween.Update();
	}


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		doTween = new DoTween();
		gridStack = proxy.GameGridStack;
		tweenFactory = proxy.TweenFactory;
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
		List<Tween> tweenList = new List<Tween>();

		grid.ForEveryObjectCall( delegate( int x, int y, object value )
		{
			GameObject gameObject = value as GameObject;

			if( gameObject != null )
			{
				Mutate mutate = gameObject.GetComponent<Mutate>();

				if( gameObject.name == Names.Monster )
					gameObject.GetComponent<Renderer>().enabled = false;

				int posY = y * proxy.Distance;
				bool hasMoved = posY != mutate.y;

				if( hasMoved )
					tweenList.Add( tweenFactory.GetBounceYOut( mutate, posY ) );
			}
		});
		

		if( tweenList.Count > 0 )
		{
			Tween tween = tweenList[ tweenList.Count - 1 ];
			tween.OnComplete += tweenOnCompleteHandler;
			
			doTween.Add( tweenList );	
		}
		else
			tweenOnCompleteHandler( null );
	}

	private void tweenOnCompleteHandler(Tween tween)
	{
		InvokeExit();
	}
}