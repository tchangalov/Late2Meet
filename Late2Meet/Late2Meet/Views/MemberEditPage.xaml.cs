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
    public partial class MemberEditPage : ContentPage
    {
        public MemberEditPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            //var member = (Member)BindingContext;
            //await App.Database.SaveMemberAsync(member);
            //await Navigation.PopAsync();
        }

    }
}