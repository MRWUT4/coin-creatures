using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSelectState : State
{
    private new Proxy proxy;
    private State state;
    private GridStack gridStack;
    private Grid coinGrid;
    private List< Dictionary<string, object> > intersectionList;


    public CoinSelectState(Proxy proxy) : base(proxy)
    {
    	this.proxy = proxy;
    }


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
     * Override interface.
     */

    public override void Enter()
    {
        initVariables();
        initGridCoins();
    }

    public override void Exit()
    {
    	initGridCoins( false );
    }


    /**
     * Private interface.
     */

    /** Create Module Variables. */
    private void initVariables()
    {
        gridStack = proxy.GameGridStack;
        coinGrid = gridStack.GetGrid( Names.Coin );
        intersectionList = new List< Dictionary<string, object> >();
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
        if( item != null )
        {
            GameObject gameObject = item as GameObject;
            Interaction interaction = ( Interaction ) gameObject.GetComponent<Interaction>();

            interaction.OnMouseDown += interactionMouseHandler;
            interaction.OnMouseOver += interactionMouseHandler;
        }
    }

    private void killCoinInteraction(int x, int y, object item)
    {
        if( item != null )
        {
            GameObject gameObject = item as GameObject;
            Interaction interaction = ( Interaction ) gameObject.GetComponent<Interaction>();
            
            interaction.OnMouseDown -= interactionMouseHandler;
            interaction.OnMouseOver -= interactionMouseHandler;
        }
    }

    private void interactionMouseHandler(MonoBehaviour monoBehaviour)
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

            showIntersectionMonster( intersection );
            animateIntersection( intersection );
            exitIfIntersectionDoesNotMatchColor( intersection );
        }
    }

    private void exitIfIntersectionDoesNotMatchColor(Dictionary<string, object> intersection)
    {
        bool intersectionMatcherListValue = getIntersectionMatchesListValue( intersection );

        if( !intersectionMatcherListValue )
        {
            proxy.IntersectionList = intersectionList;
            InvokeExit();
        }
    }


    /** Animate selected item. */
    private void showIntersectionMonster(Dictionary<string, object> intersection)
    {
        GameObject gameObject = Helper.GetIntersectionGameObject( intersection, Names.Monster );
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    private void animateIntersection(Dictionary<string, object> intersection)
    {
        Animator coinAnimator = Helper.GetIntersectionAnimator( intersection, Names.Coin );
        Animator monsterAnimator = Helper.GetIntersectionAnimator( intersection, Names.Monster );

        coinAnimator.Play( Names.AnimationCoinSpinOut );
        monsterAnimator.Play( Names.AnimationIdle );
    }
}