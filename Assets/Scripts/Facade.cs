using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

public class Facade : MonoBehaviour
{
	// [Serializable]
	// public struct StateStruct 
	// {
	// 	public string id;
	// 	public string idNext;
	// 	public MonoScript state;
	// }

	// public string startState;
	public Proxy proxy = new Proxy();
	// public StateStruct[] stateScriptArray;

	// private Dictionary<string, State> stateDicitonary;
	private StateMachine stateMachine;
	private Names names;


	/**
	 * Public interface.
	 */

	void Start() 
	{
		// initStateDictionary();
		initVariables();
		initStateMachine();
	}

	void FixedUpdate() 
	{
		stateMachine.FixedUpdate();
	}


	/**
	 * Private interface.
	 */

	/** State dictionary functions. */
	// private void initStateDictionary()
	// {
	// 	stateDicitonary = new Dictionary<string, State>();

	// 	for( int i = 0; i < stateScriptArray.Length; ++i )
	// 	{
	// 	    StateStruct stateStruct = stateScriptArray[ i ];
	// 	    State state = createState( stateStruct.state );
	// 	    string id = stateStruct.id;

	// 	    stateDicitonary.Add( id, state );
	// 	}
	// }

	// private State createState(MonoScript monoScript)
	// {
	//     Type type = monoScript.GetClass();

	//     GameObject gameObject = new GameObject();
	//     gameObject.transform.parent = proxy.Container.transform;

	//     object[] parameters = new object[] { gameObject, proxy };
	//     State state = Activator.CreateInstance( type, parameters ) as State;

	//     return state;
	// }


	/** Variables. */
	private void initVariables()
	{
		
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.AddState( Names.GameState, new GameState( new GameObject(), proxy ) );
		stateMachine.SetState( Names.GameState );
		// for( int i = 0; i < stateDicitonary.Count; ++i )
		// {
		//     KeyValuePair<string, State> pair = stateDicitonary.ElementAt( i );
		//     stateMachine.AddState( pair.Key, pair.Value );
		// }

		// stateMachine.SetState( startState );
	}
}