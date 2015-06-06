using UnityEngine;

public class GameState : GameObjectState
{
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

	/** Create gameObject components. */
	private void initComponents()
	{
		gameObject.AddComponent<GameSetup>();
	}
}