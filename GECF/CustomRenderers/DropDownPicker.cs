using System;
namespace GECF.CustomRenderers
{
	public class DropDownPicker: Picker
    {
        /// <summary>
        /// selectedBackground Color of picker item selected
        /// </summary>
        public static readonly BindableProperty SelectedBackgroundColorProperty =
            BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(DropDownPicker), Colors.Red);

        public Color SelectedBackgroundColor
        {
            get
            {
                return (Color)GetValue(SelectedBackgroundColorProperty);
            }
            set
            {
                this.SetValue(SelectedBackgroundColorProperty, value);
            }
        }
    }
}

