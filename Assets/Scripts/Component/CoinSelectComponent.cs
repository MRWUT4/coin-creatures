using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSelectComponent : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    private GridStack gridStack;
    private Grid coinGrid;
    private List< Dictionary<string, object> > intersectionList;


    /**
     * Getter / Setter.
     */
    
    private bool getIntersectionIsValid(Dictionary<string, object> intersection)
    {   
        bool listDoesNotContainValue = getIntersectionListContainsValue( intersection ) == null;
        return listDoesNotContainValue;
    }

    private bool getIntersectionMatchesListValue(Dictionary<string, object> intersection0)
    {
        Dictionary<string, object> intersection1 = intersectionList[ 0 ];

        GameObject gameObject0 = intersection0[ Names.Monster ] as GameObject;
        GameObject gameObject1 = intersection1[ Names.Monster ] as GameObject;
        
        bool lastValueMatchesListValue = gameObject0.name == gameObject1.name;

        return lastValueMatchesListValue;

    }

    private Dictionary<string,object> getIntersectionListContainsValue(Dictionary<string,object> intersection)
    {
        for( int i = 0; i < intersectionList.Count; ++i )
        {
            Dictionary<string,object> item = intersectionList[ i ];

            if( item[ Names.Monster ] == intersection[ Names.Monster ] )
                return intersection;
        }

        return null;
    }

    private Dictionary<string, object> getIntersection(MonoBehaviour monoBehaviour)
    {
        GameObject coinGameObject = monoBehaviour.gameObject;
        Point point = coinGrid.GetPositionOfObject( coinGameObject );
        Dictionary<string, object> intersection = gridStack.GetIntersection( point.x, point.y );

        return intersection;
    }



    /**
     * Component interface.
     */

    public void Start()
    {
        initVariables();
        iniStateEventHandler();
        initGridCoins();
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        state = gameObject.GetComponent<StateInfo>().state;
        proxy = state.proxy as Proxy;
        gridStack = proxy.GameGridStack;
        coinGrid = gridStack.GetGrid( Names.Coin );
        intersectionList = new List< Dictionary<string, object> >();
    }


    /** State event handler. */
    private void iniStateEventHandler()
    {
        state.OnExit += stateOnExitHandler;
    }

    private void stateOnExitHandler(State state)
    {
        initGridCoins( false );
    }


    /** Init grid coin functions. */
    private void initGridCoins(bool boolean = true)
    {
        if( boolean )
            coinGrid.ForEveryObjectCall( setupCoinInteraction );
        else
            coinGrid.ForEveryObjectCall( killCoinInteraction );
    }

    private void setupCoinInteraction(int x, int y, object item)
    {
        GameObject gameObject = item as GameObject;
        InteractionObject interactionObject = ( InteractionObject ) gameObject.GetComponent<InteractionObject>();

        interactionObject.OnMouseDown += interactionObjectMouseHandler;
        interactionObject.OnMouseOver += interactionObjectMouseHandler;
    }

    private void killCoinInteraction(int x, int y, object item)
    {
        GameObject gameObject = item as GameObject;
        InteractionObject interactionObject = ( InteractionObject ) gameObject.GetComponent<InteractionObject>();
        
        interactionObject.OnMouseDown -= interactionObjectMouseHandler;
        interactionObject.OnMouseOver -= interactionObjectMouseHandler;
    }

    private void interactionObjectMouseHandler(MonoBehaviour monoBehaviour)
    {
        validateSelectedMonoBehaviour( monoBehaviour );
    }
    

    /** Monster validation functions. */
    private void validateSelectedMonoBehaviour(MonoBehaviour monoBehaviour)
    {
        Dictionary<string, object> intersection = getIntersection( monoBehaviour );
        bool intersectionIsValid = getIntersectionIsValid( intersection );

        if( intersectionIsValid )
        {
            intersectionList.Add( intersection );

            animateIntersection( intersection );
            exitIfIntersectionDoesNotMatchColor( intersection );
        }
    }

    private void exitIfIntersectionDoesNotMatchColor(Dictionary<string, object> intersection)
    {
        bool intersectionMatcherListValue = getIntersectionMatchesListValue( intersection );

        if( !intersectionMatcherListValue )
        {
            Debug.Log( "InvokeExit" );
            state.InvokeExit();
        }
    }


    /** Animate selected item. */
    private void animateIntersection(Dictionary<string, object> intersection)
    {
        GameObject coinGameObject = intersection[ Names.Coin ] as GameObject;
        Animator coinAnimator = coinGameObject.GetComponent<Animator>();

        coinAnimator.Play( Names.AnimationCoinSpinOut );
    }
}