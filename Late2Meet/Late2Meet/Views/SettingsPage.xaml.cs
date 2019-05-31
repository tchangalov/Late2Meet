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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        async void OnMemberAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemberEntryPage
            {
                BindingContext = new Member()
            });
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    string text = MainEntry.Text;

        //    MainLabel.Text = "Hello" + text;
        //}
    }
}