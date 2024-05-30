using System;
using static Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.VisualElement;

namespace GECF.Models
{
    public class ShadowNavigationPage : NavigationPage
    {
        public ShadowNavigationPage(Page page) : base(page)
        {
            this.BarTextColor = Colors.White;
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();

            Effects.Add(new GECF.Effects.ShadowEffect()
            {
                Radius = 5,
                DistanceX = 0,
                DistanceY = 0,
                Color = Colors.Black
            });
        }
    }
}

