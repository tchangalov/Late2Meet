using Late2Meet.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Late2Meet.Views
{
    public partial class TransparentPopupPage : PopupPage
    {
        LeaderboardPage _parent;
        private IList<object> _selectedItems;

        public TransparentPopupPage()
        {
            InitializeComponent();
        }

        public TransparentPopupPage(LeaderboardPage leaderboardPage)
        {
            InitializeComponent();

            _parent = leaderboardPage;
            _selectedItems = _parent.getSelectedItems();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            entryButton.Focus();
        }

        private void switchToggle(object sender, ToggledEventArgs e)
        {
            isNegative.Text = e.Value ? "-" : "";
            if (mySwitch.IsToggled)
            {
                btnAdd.Text = "Subtract";
            } else
            {
                btnAdd.Text = "Add";

            }
        }

        private void BtnAddClicked(object sender, EventArgs eventArgs)
        {
            EntryButton_Unfocused(null, null);
            IList<object> members = _selectedItems;
            var tasks = new List<Task<int>>();

            foreach (var member in members)
            {
                if (!string.IsNullOrEmpty(entryButton.Text))
                {
                    string result = mySwitch.IsToggled ? "-" + entryButton.Text : entryButton.Text;
                    tasks.Add(_parent.BalanceAddAux((Member)member, Config.ValueAsDecimal(result)));
                }
                else
                {
                    return;
                }
            }

            exit();
        }

        private void BtnSetClicked(object sender, EventArgs eventArgs)
        {
            EntryButton_Unfocused(null, null);
            IList<object> members = _selectedItems;
            var tasks = new List<Task<int>>();

            foreach (var member in members)
            {
                if (!string.IsNullOrEmpty(entryButton.Text))
                {
                    string result = mySwitch.IsToggled ? "-" + entryButton.Text : entryButton.Text;
                    tasks.Add(_parent.BalanceSetAux((Member)member, Config.ValueAsDecimal(result)));
                }
                else
                {
                    return;
                }
            }

            exit();
        }

        private void EntryButton_Unfocused(object sender, FocusEventArgs e)
        {
            entryButton.Focus();
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();
            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }


        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private void exit()
        {
            Task.WhenAll();
            _parent.DeselectEntities();
            PopupNavigation.PopAsync();
        }

        //protected override async Task OnDisappearingAnimationBeginAsync()
        //{
        //    //if (!IsAnimationEnabled)
        //    //    return;

        //    //var taskSource = new TaskCompletionSource<bool>();

        //    //var currentHeight = FrameContainer.Height;

        //    //FrameContainer.Animate("HideAnimation", d =>
        //    //{
        //    //    FrameContainer.HeightRequest = d;
        //    //},
        //    //start: currentHeight,
        //    //end: 170,
        //    //finished: async (d, b) =>
        //    //{
        //    //    await Task.Delay(300);
        //    //    taskSource.TrySetResult(true);
        //    //});

        //    //await taskSource.Task;
        //}

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            //FrameContainer.HeightRequest = -1;

            //if (!IsAnimationEnabled)
            //{
            //    CloseImage.Rotation = 0;
            //    CloseImage.Scale = 1;
            //    CloseImage.Opacity = 1;

            //    return;
            //}

            //CloseImage.Rotation = 30;
            //CloseImage.Scale = 0.3;
            //CloseImage.Opacity = 0;
        }

        //protected override async Task OnAppearingAnimationEndAsync()
        //{
        //    //await Task.WhenAll(
        //    //    CloseImage.FadeTo(1),
        //    //    CloseImage.ScaleTo(1, easing: Easing.SpringOut),
        //    //    CloseImage.RotateTo(0));
        //}
    }
}