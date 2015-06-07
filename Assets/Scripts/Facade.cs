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
		public string idNext;
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
		stateMachine.FixedUpdate();
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
		    State state = createState( stateStruct.state );
		    string id = stateStruct.id;

		    stateDicitonary.Add( id, state );
		}
	}

	private State createState(MonoScript monoScript)
	{
	    Type type = monoScript.GetClass();
	    object[] parameters = new object[] { new GameObject(), proxy };
	    State state = Activator.CreateInstance( type, parameters ) as State;

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
		Debug.Log( "State " + state.id );		
	}
}