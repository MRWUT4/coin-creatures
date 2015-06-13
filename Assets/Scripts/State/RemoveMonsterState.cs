using UnityEngine;

public class RemoveMonsterState : State
{
	Proxy proxy;

	public RemoveMonsterState(Proxy proxy) : base(proxy)
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
		// If remove list is longer than 2 remove all except last element.
	}
}