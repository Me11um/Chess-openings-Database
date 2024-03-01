using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using FinalProjectFR.Droid;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavAnimationRenderer))]
namespace FinalProjectFR.Droid
{
    public class NavAnimationRenderer : NavigationPageRenderer
    {
        public NavAnimationRenderer(Context context) : base(context)
        {
            
        }
        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            return base.OnPushAsync(view, false);
        }
        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            return base.OnPopViewAsync(page, false);
        }
        protected override Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            return base.OnPopToRootAsync(page, false);
        }
    }
}