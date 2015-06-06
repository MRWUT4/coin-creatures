using UnityEngine;

public class GameSetup : MonoBehaviour
{
	/**
	 * Component interface.
	 */

	public void Start()
	{
		initVariables();	
	}

	public void FixedUpdate()
	{
		
	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		State state = gameObject.GetComponent<StateInfo>().state;
		Proxy proxy = state.proxy as Proxy;
		state.InvokeExit();

		Debug.Log( proxy.Container );
	}

	private void stateOnExitHandler(State state)
	{
		Debug.Log( "stateOnExitHandler");
	}
}