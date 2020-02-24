using System;
using Xamarin.Forms;
using Late2Meet.Models;
using Xamarin.Forms.Xaml;
using Java.Lang;

namespace Late2Meet.Views
{
    public partial class MemberEntryPage : ContentPage
    {
        public MemberEntryPage(SettingsPage parent)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            editorButton.Focus();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var member = new Member();
            member.Name = editorButton.Text;
            member.Balance = 0;

            if(member.Name.Length == 0)
            {
                await DisplayAlert("Error", "Empty Name, try again!", "OK");

            }
            else
            {
                await App.Database.SaveMemberAsync(member);
                await DisplayAlert("Success!", string.Format("Added {0}.", member.Name), "OK");
                editorButton.Text = "";
            }
        }

        //private void Editor_Unfocused(object sender, FocusEventArgs e)
        //{
        //    editorButton.Focus();
        //}
    }
}
