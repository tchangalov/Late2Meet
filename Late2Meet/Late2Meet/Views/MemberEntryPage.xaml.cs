using System;
using Xamarin.Forms;
using Late2Meet.Models;

namespace Late2Meet.Views
{
    public partial class MemberEntryPage : ContentPage
    {
        public MemberEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var member = (Member)BindingContext;
            await App.Database.SaveMemberAsync(member);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var member = (Member)BindingContext;
            await App.Database.DeleteMemberAsync(member);
            await Navigation.PopAsync();
        }
    }
}
