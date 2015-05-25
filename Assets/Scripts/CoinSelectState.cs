using UnityEngine;

public class CoinSelectState : State
{
	private Proxy proxy;

	public CoinSelectState(Proxy proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
	}

	public override void Exit()
	{

	}

	public override void Kill()
	{

	}

	public override void Update()
	{

	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		Debug.Log( "CoinSelectState.initVariables" );	
	}
}