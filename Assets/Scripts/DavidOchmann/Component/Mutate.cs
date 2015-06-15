using UnityEngine;

public class Mutate : MonoBehaviour
{
	private Vector3 position;
	private SpriteRenderer _spriteRenderer = null;

	// private SpriteRenderer spriteRenderer;


	/**
	 * Override interface.
	 */

	void Awake()
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
	public SpriteRenderer spriteRenderer
	{
		get 
	    { 
	    	_spriteRenderer = _spriteRenderer != null ? _spriteRenderer : gameObject.GetComponent<SpriteRenderer>();
	        return _spriteRenderer; 
	    }
	}

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
	    	Color spriteRendererColor = color;
	    
	    	spriteRendererColor.a = value;
	        spriteRenderer.color = spriteRendererColor; 
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
	}
}