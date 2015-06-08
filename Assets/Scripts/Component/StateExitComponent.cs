using UnityEngine;

public class StateExitComponent : MonoBehaviour
{
    private State state;
    private Proxy proxy;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        initStateExit();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;       
    }


    /** Exit the current State. */
    private void initStateExit()
    {
    	state.InvokeExit();
    }
}