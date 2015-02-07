
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MedNote_Droid
{
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

