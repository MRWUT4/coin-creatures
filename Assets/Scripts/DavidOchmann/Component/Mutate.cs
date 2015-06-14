using UnityEngine;

public class Mutate : MonoBehaviour
{
	private Vector3 position;
	private SpriteRenderer spriteRenderer;


	/**
	 * Override interface.
	 */

	void Start()
	{
		initVariables();
	}


	/**
	 * Getter / Setter.
	 */


	/** Vector3 properties. */
	public float x
	{
		set 
		{ 
			position.x = value;
			gameObject.transform.position = position;
		}
		
		get 
		{ 
			return position.x; 
		}
	}

	public float y
	{
		set 
		{ 
			position.y = value;
			gameObject.transform.position = position;
		}
		
		get 
		{ 
			return position.y; 
		}
	}


	/** Sprite rendere properties. */	
	public string sortingLayerName
	{
		set 
		{ 
			spriteRenderer.sortingLayerName = value; 
		}
		
		get 
		{ 
			return spriteRenderer.sortingLayerName;
		}
	}

	public Color color
	{
		set
	    { 
	       spriteRenderer.color = value; 
	    }
		
		get 
	    { 
	        return spriteRenderer.color; 
	    }
	}

	public float alpha
	{
		set
	    { 
	    	// Debug.Log( spriteRenderer );
	    	// color.a = value;
	     //    spriteRenderer.color = color; 
	    }
		
		get 
	    { 
	    	return color.a;
	    }
	}

	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		position = gameObject.transform.position;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		// Debug.Log( spriteRenderer );
	}
}