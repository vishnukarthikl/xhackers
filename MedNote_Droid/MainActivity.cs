
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Net;
using System;
using Android.Graphics;
using Java.IO;

namespace MedNote_Droid
{
	[Activity (Label = "MedNote_Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		File dir;
		File file;
		ImageView tabletImageView;
		Bitmap bitmap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var addButton = FindViewById<Button> (Resource.Id.addTimeButton);
			var dosageLayout = FindViewById<LinearLayout> (Resource.Id.dosageLinearLayout);

			addButton.Click += delegate {
				DialogFragment newFragment = new TimePickerFragment (this,dosageLayout);
				newFragment.Show (this.FragmentManager, "timePicker");
			};

			var takepicButton = FindViewById<Button> (Resource.Id.takepic);
			tabletImageView = FindViewById<ImageView> (Resource.Id.tabletImageView);

			if (bitmap != null) {
				tabletImageView.SetImageBitmap (bitmap);
				bitmap = null;
			}

			takepicButton.Click += delegate {
				Intent intent = new Intent(MediaStore.ActionImageCapture);
				dir = new File (Android.OS.Environment.GetExternalStoragePublicDirectory (Android.OS.Environment.DirectoryPictures), "MedNote");
				if (!dir.Exists())
				{
					dir.Mkdirs();
				}
				file = new File (dir, String.Format ("tablet_{0}.jpg", Guid.NewGuid ()));
				intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));
				StartActivityForResult(intent, 0);
			};

		}



		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.
			int height = Resources.DisplayMetrics.HeightPixels;
			int width =  tabletImageView.Width ;
			bitmap = BitmapHelpers.LoadAndResizeBitmap(file.AbsolutePath,width, height);
			tabletImageView.SetImageBitmap (bitmap);
		}
	}

}


