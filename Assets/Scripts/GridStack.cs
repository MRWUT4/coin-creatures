using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GridStack
{
	public int width;
	public int height;

	private Dictionary<string, Grid> dictionary = new Dictionary<string, Grid>();

	public GridStack(int width, int height)
	{
		this.width = width;
		this.height = height;
	}


	public Grid AddGrid(string id)
	{
		var grid = new Grid( this.width, this.height );
		dictionary.Add( id, grid );

		return grid;
	}

	public Grid GetGrid(string id)
	{
		return dictionary[ id ];
	}

	public Dictionary<string, object> GetIntersection(int x, int y)
	{
		Dictionary<string, object> intersection = new Dictionary<string, object>();

		for( int i = 0; i < dictionary.Count; ++i )
		{
			KeyValuePair<string, Grid> pair = dictionary.ElementAt( i );
			string key = pair.Key;
			object value = pair.Value.Get( x, y );

			intersection.Add( key, value );
		}

		return intersection;
	}
}
