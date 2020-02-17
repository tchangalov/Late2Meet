using Late2Meet.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace Late2Meet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalysisPage : ContentPage
    {
        private decimal total;
        private List<Member> members;

        public AnalysisPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                total = await App.Database.GetTotalBalanceAsync();
            }
            catch (NullReferenceException e)
            {
                total = 0;
            }

            members = await App.Database.GetMembersOrderByNameAsync();

            var entries = new Entry[members.Count];


            int i = 0;
            foreach (var member in members)
            {
                decimal pct = (member.Balance / total) * 100;
                var random = new Random(member.Id);
                string color = String.Format("#{0:X6}", random.Next(0x1000000));

                string label = null;
                if (pct.ToString().Length >= 5)
                {
                    label = pct.ToString().Substring(0, 5) + "%";
                }
                else
                {
                    label = pct.ToString() + "%";
                }

                entries[i] = new Entry((float)pct)
                {
                    Label = member.Name,
                    ValueLabel = label,
                    Color = SKColor.Parse(color)
                };

                i++;
            }

          

            var chart = new DonutChart() { Entries = entries, LabelTextSize = 30 };
            this.chartView.Chart = chart;
        }
    }
}