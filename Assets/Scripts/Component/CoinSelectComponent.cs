using System.Collections;
using UnityEngine;

public class CoinSelectComponent : MonoBehaviour
{
    private State state;
    private Proxy proxy;
    private GridStack gameGridStack;
    private Grid coinGrid;
    private ArrayList selectionList;


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
        gameGridStack = proxy.GameGridStack;
        coinGrid = gameGridStack.GetGrid( Names.Coin );
        selectionList = new ArrayList();
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
        object listContainsValue = Assist.ListContainsValue( selectionList, monoBehaviour );

        if( listContainsValue == null )
        {
            selectionList.Add( monoBehaviour );

            parseSelectionListForEnd();
            animateItemTo( monoBehaviour );
        }
    }


    /** Pase selection and exit if last entry is Wrong. */
    private void parseSelectionListForEnd()
    {
        
    }


    /** Animate selected item. */
    private void animateItemTo(MonoBehaviour monoBehaviour)
    {
        Animator animator = monoBehaviour.GetComponent<Animator>();

        animator.Play( Names.AnimationCoinSpinOut );
    }
}