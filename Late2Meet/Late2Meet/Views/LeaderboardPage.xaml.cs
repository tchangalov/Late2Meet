using Late2Meet.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Late2Meet;
using Forms9Patch;

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
            Xamarin.Forms.Button button = (Xamarin.Forms.Button)Sender;
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


        public async void onShareButtonClicked(object sender, EventArgs e)
        {

            var htmlString = constructHtml();

            if (await htmlString.ToPngAsync("output.png") is ToFileResult pngResult)
            {
                if (pngResult.IsError)
                {
                    using (Toast.Create("PNG Failure", pngResult.Result)) ;
                }
                else
                {
                    var collection = new Forms9Patch.MimeItemCollection();
                    collection.AddBytesFromFile("image/png", pngResult.Result);
                    //Forms9Patch.Clipboard.Entry = collection;
                    Forms9Patch.Sharing.Share(collection, shareButton);
                }
            }
        }

        /// <summary>
        /// Adapted using https://www.tablesgenerator.com/html_tables#
        /// </summary>
        /// <returns></returns>
        private string constructHtml()
        {
            var startTag = "<html><body>";
            var title = "<h1 style=\"text-align:center;padding-top:10px;\">Late2Meet</h1>";
            var tableStart = "<style type=\"text/css\">" +
                            ".tg  {border-collapse:collapse;border-spacing:0;}" +
                            ".tg td{font-family:Arial,sans-serif;font-size:14px;padding:5px5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}" +
                            ".tg th{font-family:Arial,sans-serif;font-size:14px;font-weight:normal;padding:5px5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;border-color:black;}" +
                            ".tg .tg-yutr{font-weight:bold;font-family:Arial,Helvetica,sans-serif!important;;color:#036400;border-color:inherit;text-align:center;vertical-align:top}" +
                            ".tg .tg-zda1{font-family:Arial,Helvetica,sans-serif!important;;border-color:inherit;text-align:center;vertical-align:top}" +
                            ".tg .tg-7fle{font-weight:bold;background-color:#efefef;text-align:center;vertical-align:top}" +
                            ".tg .tg-ud39{font-weight:bold;color:#036400;text-align:center;vertical-align:top}" +
                            ".tg .tg-0d3g{font-weight:bold;font-family:Arial,Helvetica,sans-serif!important;;background-color:#efefef;border-color:inherit;text-align:center;vertical-align:top}" +
                            "</style>" +
                            "<table align=\"center\" class=\"tg\">" +
                            "  <tr>" +
                            "    <th class=\"tg-7fle\" colspan=\"2\">Total</th>" +
                            "  </tr>" +
                            "  <tr>" +
                            "    <td class=\"tg-ud39\" colspan=\"2\">" + string.Format("{0:$#,##0.00}", CurrentTotalAmount) + "</td>" +
                            "  </tr>" +
                            "  <tr>" +
                            "    <td class=\"tg-0d3g\">Name</td>" +
                            "    <td class=\"tg-0d3g\">Balance</td>" +
                            "  </tr>";

            string addMembers = "";

            foreach (var member in listView.ItemsSource)
            {
                addMembers += string.Format("<tr><td class=\"tg-zda1\">{0}</td><td class=\"tg-yutr\">{1}</td></tr>",
                    ((Member)member).Name,
                    string.Format("{0:$#,##0.00}", ((Member)member).Balance));
            }


            string timeStamp = DateTime.Now.ToString();
            var subTitle = String.Format("<p style=\"font-size:10px;text-align:center;\"> As of: {0}</p>", timeStamp);

            var tableEnd = "</table>";
            var endTag = "</body></html>";

            return startTag + title + tableStart + addMembers + tableEnd + subTitle + endTag;
        }
    }
}