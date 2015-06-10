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

    private Dictionary<string,object> getIntersectionListContainsValue(Dictionary<string,object> intersection)
    {
        for( int i = 0; i < intersectionList.Count; ++i )
        {
            Dictionary<string,object> item = intersectionList[ i ];
            
            if( item == intersection )
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


    /** Init grid coin functions. */
    private void initGridCoins()
    {
        coinGrid.ForEveryObjectCall( setupCoin );
    }

    private void setupCoin(int x, int y, object item)
    {
        GameObject gameObject = item as GameObject;
        InteractionObject interactionObject = ( InteractionObject ) gameObject.GetComponent<InteractionObject>();

        interactionObject.OnMouseDown -= interactionObjectMouseHandler;
        interactionObject.OnMouseOver -= interactionObjectMouseHandler;

        interactionObject.OnMouseDown += interactionObjectMouseHandler;
        interactionObject.OnMouseOver += interactionObjectMouseHandler;
    }

    private void interactionObjectMouseHandler(MonoBehaviour monoBehaviour)
    {
        Dictionary<string, object> intersection = getIntersection( monoBehaviour );
        bool intersectionIsValid = getIntersectionIsValid( intersection );

        if( intersectionIsValid )
        {
            intersectionList.Add( intersection );


        }
    }
    

    /** Animate selected item. */
    private void animateIntersecion(Dictionary<string, object> intersection)
    {
        GameObject coinGameObject = intersection[ Names.Coin ] as GameObject;
        Animator coinAnimator = coinGameObject.GetComponent<Animator>();

        coinAnimator.Play( Names.AnimationCoinSpinOut );
    }
}