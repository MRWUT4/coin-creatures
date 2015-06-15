public class TweenFactory
{
	private DoTween doTween;
	private float duration;
	private float delay;


	public TweenFactory(float duration, float delay)
	{
		this.duration = duration;
		this.delay = delay;

		initVariables();
	}


	/**
	 * Public interface.
	 */

	public Tween GetBackAlphaOut(Mutate target, int index)
	{
		return doTween.To( target, duration, new 
		{ 
			delay = index * delay, 
			y = target.y + 5, 
			alpha = 0, 
			ease = "Back.EaseIn" 
		});
	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		doTween = new DoTween();
		doTween.isFactory = true;
	}
}