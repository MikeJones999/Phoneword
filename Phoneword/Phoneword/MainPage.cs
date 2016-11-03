using Phoneword.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoneword
{
    class MainPage : ContentPage
    {

        Button button1;
        Button button2;
        Label label;
        Entry entry;
        string translatedNumber;

        public MainPage()
        {

            this.Padding = new Thickness(20,
            Device.OnPlatform<double>(40, 20, 20), 20, 20);


            button1 = new Button
            {
                Text = "Translate",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button1.Clicked += OnButtonClicked1;


            button2 = new Button
            {
                Text = "Call",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsEnabled = false
            };
            button2.Clicked += OnCall;


            label = new Label
            {
                Text = "Enter a Phoneword",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            entry = new Entry
            {
                Placeholder = "e.g. 1-855-XAMARIN"
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,

                Children =
                {
                    label,
                    entry,             
                    button1,
                    button2
                    
                }
            };         
        }

        private void OnButtonClicked1(object sender, EventArgs e)
        {
           
            translatedNumber = PhonewordTranslator.ToNumber(entry.Text);
            if(!string.IsNullOrEmpty(translatedNumber))
            {
                button2.IsEnabled = true;
                button2.Text = "Call " + translatedNumber;
            }
            else
            {
                button2.IsEnabled = false;
                button2.Text = "Call";
            }
        }

        public async void  OnCall(object sender, EventArgs e)
        {

            var task = this.DisplayAlert("Dial a Number", "Would you like to dial " + translatedNumber + "?", "Yes", "No");

            if(await task)
            {
                //factory method for retrieving platform-specific implementations of the specified type T - in this case IDialer (interface)
                var dialer = DependencyService.Get<IDialer>();
                if(dialer != null)
                {
                    await dialer.DialAsync(translatedNumber);
                }
              
            }
            else
            {

            }
        }
    }
}
