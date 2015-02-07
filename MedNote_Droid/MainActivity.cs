using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MedNote_Droid
{
	[Activity (Label = "MedNote_Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private static int dosageTimeStartId = 100;

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
		}
			
	}

	public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener 
	{
		private Context context;
		private int dosageId = 100;
		private LinearLayout dosageTimeLayout;

		public TimePickerFragment (Context context,LinearLayout dosageLayout)
		{
			this.dosageTimeLayout = new LinearLayout (context);
			dosageLayout.AddView (dosageTimeLayout);
			this.context = context;		
		}

		override
		public Dialog OnCreateDialog(Bundle savedInstanceState) {
			// Use the current time as the default values for the picker
			var currentTime = DateTime.Now;
			int hour = currentTime.Hour;
			int minute = currentTime.Minute;

			// Create a new instance of TimePickerDialog and return it
			return new TimePickerDialog(Activity, this, hour, minute,true);
		}

		public void OnTimeSet(TimePicker view, int hourOfDay, int minute) {
			var editText = new EditText (context);
			editText.Id = dosageId++;
			editText.Text = string.Format ("{0}:{1}", hourOfDay, minute);
			dosageTimeLayout.AddView (editText);
		}
	}


}


