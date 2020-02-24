using Forms9Patch;
using Late2Meet.Models;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;
using Image = Xamarin.Forms.Image;

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
            setContributionPercentage();
        }

        //public async void onShareButtonClicked(object sender, EventArgs e)
        //{
        //    int width = Convert.ToInt32(chartView.Width);
        //    int height = Convert.ToInt32(chartView.Height);
        //    var bmp = new SKBitmap(width, height);
        //    var canvas = new SKCanvas(bmp);
        //    chartView.Chart.DrawContent(canvas, width, height);
        //    canvas.Save();

        //    using (var image = SKImage.FromBitmap(bmp)) // tried SKImage.FromBitmap(bmp) first and same result
        //    using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
        //    {
        //        var encoded = ImageUtility.StreamToString(data.AsStream());
        //        byte[] imageBytes;
        //        var FileImage = new Image();
        //        imageBytes = Convert.FromBase64String(encoded);
        //        FileImage.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageBytes));
        //        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "output_analysis.png");
        //        File.WriteAllBytes(path, imageBytes);
        //        var collection = new Forms9Patch.MimeItemCollection();
        //        collection.AddBytesFromFile("image/png", path);
        //        Forms9Patch.Sharing.Share(collection, shareButton);
        //    }
        //}

        private async void setContributionPercentage()
        {
            try
            {
                total = await App.Database.GetTotalBalanceAsync();
            }
            catch (NullReferenceException)
            {
                total = 0;
            }

            members = await App.Database.GetMembersOrderByNameAsync();
            var entries = new Entry[members.Count];

            int i = 0;
            foreach (var member in members)
            {
                if(total == 0)
                {
                    return;
                }
                decimal pct = (member.Balance / total) * 100;
                var random = new Random(member.Id + 100);
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

            var chart = new DonutChart() { Entries = entries };

            chartView.Chart = chart;
            chartView.Chart.LabelTextSize = (float)35;
            int magicLabelMargin = (int)130;
            chartView.HeightRequest = chartView.Width - magicLabelMargin;
        }
    }
}