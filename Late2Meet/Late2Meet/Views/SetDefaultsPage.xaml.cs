using Late2Meet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Late2Meet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetDefaultsPage : ContentPage
    {
        public SetDefaultsPage()
        {

            InitializeComponent();
            entryButton.Text = Config.ValueAsString(Config.AddValue);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async void OnSaveButtonClickedAsync(Object Sender, EventArgs args)
        {
            var defaultAddValueEntry = new Entry
            {
                Text = entryButton.Text
            };
            //string result = mySwitch.IsToggled ? "-" + entryButton.Text : entryButton.Text;
            Config.AddValue = Config.ValueAsDecimal(entryButton.Text);
            await Navigation.PopAsync();
        }

        //private void switchToggle(object sender, ToggledEventArgs e)
        //{
        //    isNegative.Text = e.Value ? "-" : "";
        //}

        //private void EntryButton_Unfocused(object sender, FocusEventArgs e)
        //{
        //    entryButton.Focus();
        //}
    }
}