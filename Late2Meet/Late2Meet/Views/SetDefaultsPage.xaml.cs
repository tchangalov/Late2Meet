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
    public partial class SetDefaultsPage : ContentPage
    {
        public SetDefaultsPage()
        {
            InitializeComponent();
            if (!Application.Current.Properties.ContainsKey(Constants.DefaultAdd))
            {
                var defaultAddValueEntry = new Entry
                {
                    Text = "1"
                };
                Application.Current.Properties[Constants.DefaultAdd] = defaultAddValueEntry;
                newDefaultValue.Text = defaultAddValueEntry.Text;
            }
            else
            {
                Entry defaultAddValueEntry = (Entry)Application.Current.Properties[Constants.DefaultAdd];
                newDefaultValue.Text = defaultAddValueEntry.Text;
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var defaultAddValueEntry = new Entry
            {
                Text = newDefaultValue.Text
            };
            Application.Current.Properties[Constants.DefaultAdd] = defaultAddValueEntry;
        }
    }
}