using System;
using UnityEngine;
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
}
