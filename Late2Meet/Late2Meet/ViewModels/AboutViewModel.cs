using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Late2Meet.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://late2meet.s3-website-us-east-1.amazonaws.com/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}