using UnityEngine;

public class GameSetupState : GameObjectState
{
	private StateMachine stateMachine;


	public GameSetupState(GameObject gameObject, Proxy proxy) : base(gameObject, proxy){}


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

	/** Add GameSetupState comopnents. */
	private void initComponents()
	{
		gameObject.AddComponent<GameSetup>();
	}
}