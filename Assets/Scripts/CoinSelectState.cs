using UnityEngine;

public class CoinSelectState : GameObjectState
{
	private StateMachine stateMachine;


	public CoinSelectState(GameObject gameObject, Proxy proxy) : base(gameObject, proxy){}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initComponents();
	}


	/**
	 * Private interface.
	 */

	/** Add CoinSelectState components. */
	private void initComponents()
	{
		gameObject.AddComponent<CoinSelectComponent>();
	}
}