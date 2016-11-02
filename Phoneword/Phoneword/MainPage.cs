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
            button1.Clicked += OnButtonClicked;


            button2 = new Button
            {
                Text = "Call",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsEnabled = false
            };
            button2.Clicked += OnButtonClicked;


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

        private void OnButtonClicked(object sender, EventArgs e)
        {
           
            var translatedNumber = PhonewordTranslator.ToNumber(entry.Text);
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
    }
}
