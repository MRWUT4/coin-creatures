using UnityEngine;

public class AddTopRowState : State
{
	private new Proxy proxy;

	public AddTopRowState(Proxy proxy) : base(proxy)
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


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		
	}
}