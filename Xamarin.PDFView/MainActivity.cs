using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.OS;

namespace Xamarin.PDFView
{
	[Activity (Label = "PDFView", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
		private  string _documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
		private  string _pdfPath;
		private  string _pdfFileName = "ThePDFDocumentName.pdf";
		private  string _pdfFilePath;
		private WebView _webView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			_pdfPath = _documentsPath + "/PDFView";
			_pdfFilePath  = Path.Combine(_pdfPath, _pdfFileName);

			// Check if the PDFDirectory Exists
			if(!Directory.Exists(_pdfPath)){
				Directory.CreateDirectory(_pdfPath);
			}
			else{
				// Check if the IDCard is there, If Yes Delete It. Because we will download the fresh one just in a moment
				if (File.Exists(_pdfFilePath)){
					File.Delete(_pdfFilePath);
				}
			}

			_webView = FindViewById<WebView> (Resource.Id.webView1);
			var settings = _webView.Settings;
			settings.JavaScriptEnabled = true;
			settings.AllowFileAccessFromFileURLs = true;
			settings.AllowUniversalAccessFromFileURLs = true;
			settings.BuiltInZoomControls = true;
			_webView.SetWebChromeClient(new WebChromeClient());
			_webView.LoadUrl("file:///android_asset/PDFViewer/index.html?file=" + _pdfFilePath);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			_webView.LoadUrl( "javascript:window.location.reload( true )" );
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_webView.ClearCache(true);
		}
	}
}


