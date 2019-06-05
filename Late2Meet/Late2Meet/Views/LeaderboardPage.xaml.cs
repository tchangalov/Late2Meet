using Late2Meet.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Late2Meet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderboardPage : ContentPage
    {

        decimal BalanceAddValue = 1;
        decimal CurrentTotalAmount;
        public LeaderboardPage()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey(Constants.DefaultAdd))
            {
                Entry defaultAddValueEntry = (Entry)Application.Current.Properties[Constants.DefaultAdd];
                BalanceAddValue = (decimal)new DecimalConverter().ConvertBack(defaultAddValueEntry.Text, null, null, null);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UpdateMembersView();
        }

        /* 
         * Gets all members for the leaderboard
         */
        private async void UpdateMembersView()
        {
            listView.ItemsSource = await App.Database.GetMembersOrderByBalanceAsync();
            try
            {
                CurrentTotalAmount = await App.Database.GetTotalBalanceAsync();
                updateTotal();
            }
            catch (NullReferenceException e)
            {
                totalAmount.Text = "$ --";
            }
        }

        public void updateTotal()
        {
            totalAmount.Text = "$ " + CurrentTotalAmount.ToString();
        }

        async void OnAdvancedBalanceAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new TransparentPopup());
        }

        //async void OnAdvancedBalanceAddClicked(object sender, EventArgs e)
        //{
        //    IList<object> members = listView.SelectedItems;
        //    var tasks = new List<Task<int>>();

        //    foreach ( var member in members)
        //    {
        //        tasks.Add(BalanceAddAux((Member) member, BalanceAddValue));
        //    }
        //    await Task.WhenAll();
        //    DeselectEntities();
        //    checkEnableOrDisable();
        //}

        public async void OnQuickBalanceAddClicked(Object Sender, EventArgs args)
        {
            Button button = (Button) Sender;
            Member member = (Member) button.CommandParameter;
            await BalanceAddAux(member, BalanceAddValue);
        }

        public async Task<int> BalanceAddAux(Member curr, decimal toAdd)
        {
            curr.Balance += toAdd;
            CurrentTotalAmount += toAdd;
            updateTotal();
            return await App.Database.SaveMemberAsync(curr);
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkEnableOrDisable();
        }

        private void DeselectEntities()
        {
            listView.SelectedItems.Clear();
        }

        void checkEnableOrDisable()
        {
            if (listView.SelectedItems == null || listView.SelectedItems.Count == 0)
            {
                advancedAddButton.IsEnabled = false;
            }
            else
            {
                advancedAddButton.IsEnabled = true;
            }
        }
    }
}