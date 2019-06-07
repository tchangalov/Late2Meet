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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UpdateMembersView();
            DeselectEntities();
            checkEnableOrDisable();
            BalanceAddValue = Config.AddValue;
        }

        /* 
         * Gets all members for the leaderboard
         */
        private async void UpdateMembersView()
        {
            listView.ItemsSource = await App.Database.GetMembersOrderByNameAsync();
            try
            {
                CurrentTotalAmount = await App.Database.GetTotalBalanceAsync();
                updateTotal();
            }
            catch (NullReferenceException e)
            {
                totalAmount.Text = "Total $ --";
            }
        }

        public void updateTotal()
        {
            totalAmount.Text = "Total " + String.Format("{0:$#,##0.00}", CurrentTotalAmount);
        }

        async void OnAdvancedBalanceAddClicked(object sender, EventArgs e)
        {
            var popup = new TransparentPopupPage(this);
            await Navigation.PushPopupAsync(popup);
        }


        public async void OnQuickBalanceAddClicked(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            Member member = (Member)button.CommandParameter;
            await BalanceAddAux(member, BalanceAddValue);
        }

        public async Task<int> BalanceAddAux(Member curr, decimal toAdd)
        {
            curr.Balance += toAdd;
            CurrentTotalAmount += toAdd;
            updateTotal();
            return await App.Database.SaveMemberAsync(curr);
        }

        public async Task<int> BalanceSetAux(Member curr, decimal newBalance)
        {
            decimal delta = newBalance - curr.Balance;
            curr.Balance = newBalance;
            CurrentTotalAmount += delta;
            updateTotal();
            return await App.Database.SaveMemberAsync(curr);
        }

        public IList<object> getSelectedItems()
        {
            return listView.SelectedItems;
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkEnableOrDisable();
        }

        public void DeselectEntities()
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