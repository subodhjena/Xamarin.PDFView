using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.OS;

namespace Xamarin.PDFView
{
	[Activity (Label = "Xamarin.PDFView", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
		private WebView webView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			webView = FindViewById<WebView> (Resource.Id.webView1);

			WebSettings settings = webView.Settings;
			settings.JavaScriptEnabled = true;
			settings.AllowFileAccessFromFileURLs = true;
			settings.AllowUniversalAccessFromFileURLs = true;
			settings.BuiltInZoomControls = true;
			webView.SetWebChromeClient (new WebChromeClient ());
			webView.LoadUrl("file:///android_asset/pdfviewer/index.html");
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			webView.LoadUrl( "javascript:window.location.reload( true )" );
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			webView.ClearCache(true);
		}
	}
}


