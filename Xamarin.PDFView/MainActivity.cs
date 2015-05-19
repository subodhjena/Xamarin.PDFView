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
	[Activity (Label = "Xamarin.PDFView", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
		private WebView webView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			string[] PDFViewerFileArray = { "compatibility.js", "customview.js", "data.json", "index.html", "minimal.css", "pdf.js", "pdf.worker.js", "pdffile.js", "samplePDF.pdf"};
			var documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments);
			var pdfViewerPath = documentsPath + "/PDFViewer";

			// Create the SubFolder id it doesn't exist
			if (!System.IO.Directory.Exists (pdfViewerPath))
				System.IO.Directory.CreateDirectory (pdfViewerPath);
			else
				Console.WriteLine ("The Directory Exists !! @ Path : "+ pdfViewerPath);


			// Enumerate through the assets and copy them to the Folder
			foreach (string fileName in PDFViewerFileArray) {

				var assetsFilePath = @"pdfviewer/" + fileName;
				var newFilePath = pdfViewerPath + @"/" + fileName;

				//Read the data from assets folder
				var data = ApplicationContext.Assets.Open (assetsFilePath);
				StreamReader stream = new StreamReader (data);
				var textualData = stream.ReadToEnd ();

				// Now that we have the data lets write it to the folder
				if (!System.IO.File.Exists (newFilePath)) {
					Console.WriteLine (newFilePath + " Doesn't exist, we have to create it");
					using (System.IO.StreamWriter file = new System.IO.StreamWriter(newFilePath)) {
						file.WriteLine (textualData);
					}
				} else {
					Console.WriteLine (newFilePath + " File Exists !, ReCreating it");
					File.Delete (newFilePath);
					using (System.IO.StreamWriter file = new System.IO.StreamWriter(newFilePath)) {
						file.WriteLine (textualData);
					}
				}
			}

			webView = FindViewById<WebView> (Resource.Id.webView1);
			WebSettings settings = webView.Settings;
			settings.JavaScriptEnabled = true;
			settings.AllowFileAccessFromFileURLs = true;
			settings.AllowUniversalAccessFromFileURLs = true;
			settings.BuiltInZoomControls = true;
			webView.SetWebChromeClient (new WebChromeClient ());
			webView.LoadUrl("file:///" + pdfViewerPath + "/index.html");
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


