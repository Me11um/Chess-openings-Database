package crc64806fb8240c7579ac;


public class NavAnimationRenderer
	extends crc64720bb2db43a66fe9.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("FinalProjectFR.Droid.NavAnimationRenderer, ChessOpeningsDatabase", NavAnimationRenderer.class, __md_methods);
	}


	public NavAnimationRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == NavAnimationRenderer.class)
			mono.android.TypeManager.Activate ("FinalProjectFR.Droid.NavAnimationRenderer, ChessOpeningsDatabase", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public NavAnimationRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == NavAnimationRenderer.class)
			mono.android.TypeManager.Activate ("FinalProjectFR.Droid.NavAnimationRenderer, ChessOpeningsDatabase", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public NavAnimationRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == NavAnimationRenderer.class)
			mono.android.TypeManager.Activate ("FinalProjectFR.Droid.NavAnimationRenderer, ChessOpeningsDatabase", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
