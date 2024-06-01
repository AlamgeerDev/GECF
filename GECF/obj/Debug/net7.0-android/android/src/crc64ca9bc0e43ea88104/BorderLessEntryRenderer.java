package crc64ca9bc0e43ea88104;


public class BorderLessEntryRenderer
	extends crc6477f0d89a9cfd64b1.EntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("GECF.Platforms.Android.CustomRenderers.BorderLessEntryRenderer, GECF", BorderLessEntryRenderer.class, __md_methods);
	}


	public BorderLessEntryRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BorderLessEntryRenderer.class) {
			mono.android.TypeManager.Activate ("GECF.Platforms.Android.CustomRenderers.BorderLessEntryRenderer, GECF", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public BorderLessEntryRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BorderLessEntryRenderer.class) {
			mono.android.TypeManager.Activate ("GECF.Platforms.Android.CustomRenderers.BorderLessEntryRenderer, GECF", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public BorderLessEntryRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BorderLessEntryRenderer.class) {
			mono.android.TypeManager.Activate ("GECF.Platforms.Android.CustomRenderers.BorderLessEntryRenderer, GECF", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
		}
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
