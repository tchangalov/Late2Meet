using System;
using Xamarin.Forms;
using Late2Meet.Models;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Late2Meet.Views
{
    public partial class MemberDeletionPage : ContentPage
    {
        public MemberDeletionPage()
        {
            InitializeComponent();
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
            listView.ItemsSource = await App.Database.GetMembersOrderByNameAsync();
        }
        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkEnableOrDisable();
        }
        void checkEnableOrDisable()
        {
            if (listView.SelectedItems == null || listView.SelectedItems.Count == 0)
            {
                deleteSelectedButton.IsEnabled = false;
            }
            else
            {
                deleteSelectedButton.IsEnabled = true;
            }
        }


        async void OnDeleteAllButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning", "Are you sure?", "Yes", "No");
            
            if(answer)
            {
                await App.Database.DeleteAllMembersAsync();
                UpdateMembersView();
                DeselectEntities();
                checkEnableOrDisable();
                deleteAllButton.IsEnabled = false;
            }
        }

        async void OnDeleteSelectedButtonClicked(object sender, EventArgs e)
        {
            IList<object> members = listView.SelectedItems;
            var tasks = new List<Task<int>>();

            foreach (var member in members)
            {
                tasks.Add(DeleteMemberAux((Member)member));
            }
            await Task.WhenAll();
            UpdateMembersView();
            DeselectEntities();
            checkEnableOrDisable();
        }

        public async Task<int> DeleteMemberAux(Member curr)
        {
            return await App.Database.DeleteMemberAsync(curr);
        }

        private void DeselectEntities()
        {
            listView.SelectedItems.Clear();
        }
    }
}
