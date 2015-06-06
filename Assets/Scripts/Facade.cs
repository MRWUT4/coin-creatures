using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;

public class Facade : MonoBehaviour
{
	[Serializable]
	public struct StateStruct 
	{
		public string id;
		public MonoScript state;
	}

	public string startState;
	public Proxy proxy = new Proxy();
	public StateStruct[] stateScriptArray;

	private Dictionary<string, State> stateDicitonary;
	private StateMachine stateMachine;



	/**
	 * Public interface.
	 */

	void Start() 
	{
		initStateDictionary();
		initStateMachine();
	}

	void FixedUpdate() 
	{
		//stateMachine.FixedUpdate();
	}


	/**
	 * Private interface.
	 */

	/** State dictionary functions. */
	private void initStateDictionary()
	{
		stateDicitonary = new Dictionary<string, State>();

		for( int i = 0; i < stateScriptArray.Length; ++i )
		{
		    StateStruct stateStruct = stateScriptArray[ i ];

		    string id = stateStruct.id;
		    State state = createState( stateStruct.state );

		    stateDicitonary.Add( id, state );
		}
	}

	private State createState(MonoScript monoScript)
	{
	    Type type = monoScript.GetClass();
	    State state = Activator.CreateInstance( type ) as State;
	    state.proxy = proxy;

	    return state;
	}


	/** StateMachine functions. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		stateMachine.OnExit += stateMachineOnExitHandler;

		for( int i = 0; i < stateDicitonary.Count; ++i )
		{
		    KeyValuePair<string, State> pair = stateDicitonary.ElementAt( i );
		    stateMachine.AddState( pair.Key, pair.Value );
		}

		stateMachine.SetState( startState );
	}

	private void stateMachineOnExitHandler(State state)
	{
		
	}
}