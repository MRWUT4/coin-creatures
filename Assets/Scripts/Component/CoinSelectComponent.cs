using UnityEngine;

public class CoinSelectComponent : MonoBehaviour
{
    private State state;
    private Proxy proxy;


    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;
        
        Debug.Log( "CoinSelectComponent.initVariables" );
    }
}