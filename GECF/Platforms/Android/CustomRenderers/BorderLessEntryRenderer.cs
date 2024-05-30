using System;
using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace GECF.Platforms.Android.CustomRenderers
{
	public class BorderLessEntryRenderer: EntryRenderer
    {
        public BorderLessEntryRenderer(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }

            Control.TextSize = 16;
        }
    }
}

