using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Late2Meet
{
    class LimitNumericEntryBehavior : PlainNumericEntryBehavior
    {

        public LimitNumericEntryBehavior()
        {
            AdditionalCheck = (e, ot) =>
            {
                // Remove negative
                e.Text = e.Text.Replace("-", "");
                
                // Limit to two decimals
                if (e.Text.Contains("."))
                {
                    string[] parts = e.Text.Split('.');
                    string wholeNumber = parts[0];
                    string decimalPlaces = parts[1];
                    e.Text = wholeNumber + "." + (decimalPlaces.Length > 2 ? decimalPlaces.Substring(0, 2) : decimalPlaces);
                }
            };
        }
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}
