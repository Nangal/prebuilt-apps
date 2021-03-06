//
//  Copyright 2012, Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using CoreGraphics;

using UIKit;

using EmployeeDirectory.Data;

namespace EmployeeDirectory.iOS
{
	public class PersonCell : UITableViewCell
	{
		static readonly UIFont NormalFont = UIFont.SystemFontOfSize (16f);
		static readonly UIFont BoldFont = UIFont.BoldSystemFontOfSize (16f);
		static readonly UIFont DetailsFont = UIFont.SystemFontOfSize (12f);

		UILabel firstNameLabel;
		UILabel lastNameLabel;
		UILabel detailsLabel;

		Person person = null;

		public Person Person {
			get {
				return person;
			}
			set {
				if (person != value) {
					person = value;
					UpdateUI ();
				}
			}
		}

		public PersonCell (string id)
			: base (UITableViewCellStyle.Default, id)
		{
			firstNameLabel = new UILabel {
				Font = NormalFont,
				TextColor = UIColor.Black,
				HighlightedTextColor = UIColor.White
			};
			ContentView.Add (firstNameLabel);

			lastNameLabel = new UILabel {
				Font = BoldFont,
				TextColor = UIColor.Black,
				HighlightedTextColor = UIColor.White
			};
			ContentView.Add (lastNameLabel);

			detailsLabel = new UILabel {
				Font = DetailsFont,
				TextColor = UIColor.Gray,
				HighlightedTextColor = UIColor.White
			};
			ContentView.Add (detailsLabel);
		}

		void UpdateUI ()
		{
			var x = 44f + 6f;

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				x += 15f; // okay, we need a more thorough iOS 7 update than this, but for now this'll have to do

			var fn = person.FirstNameAndInitials;
			firstNameLabel.Text = fn;
			var fnw = string.IsNullOrEmpty (fn) ? 
				0.0f : 
					UIStringDrawing.StringSize (fn, NormalFont).Width;
			var f = new CGRect (x, 4f, fnw + 4f, 20f);
			firstNameLabel.Frame = f;

			var ln = person.SafeLastName;
			lastNameLabel.Text = ln;
			var lnw = string.IsNullOrEmpty (ln) ?
				0f : UIStringDrawing.StringSize (ln, BoldFont).Width;
			f.X = f.Right;
			f.Width = lnw;
			lastNameLabel.Frame = f;

			detailsLabel.Text = person.TitleAndDepartment ?? string.Empty;
			detailsLabel.Frame = new CGRect (x, 25f, 258f, 14f);
		}
	}
}

