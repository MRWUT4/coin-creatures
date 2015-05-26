using UnityEngine;

public abstract class Setup
{
	public delegate double EaseDelegate( double t, double b, double c, double d, double a, double p ); 

	public EaseDelegate ease;
}

public class Tween
{

	private GameObject gameObject;
	private double duration;
	private Setup setup;

	private Setup.EaseDelegate ease;

	public Tween(GameObject gameObject, double duration, Setup setup)
	{
		this.gameObject = gameObject;
		this.duration = duration;
		this.setup = setup;

		this.ease = setup.ease;
	}
}