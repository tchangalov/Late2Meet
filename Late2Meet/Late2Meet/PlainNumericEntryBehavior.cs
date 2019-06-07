using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Late2Meet
{
    class PlainNumericEntryBehavior : Behavior<Entry>
    {
        /// <summary>
        /// Check about value entered in the entry and convert it
        /// </summary>
        protected Action<Entry, string> AdditionalCheck;

        public static readonly BindableProperty AttachBehaviorProperty =
       BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(PlainNumericEntryBehavior), false, propertyChanged: TextChanged_Handler);

        private static void TextChanged_Handler(BindableObject bindable, object oldValue, object newValue)
        {
            throw new NotImplementedException();
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.TextChanged += TextChanged_Handler;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
        }
        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        public void TextChanged_Handler(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
                return;

            decimal _;
            if (!decimal.TryParse(e.NewTextValue, out _))
                ((Entry)sender).Text = e.OldTextValue;
            else
                AdditionalCheck?.Invoke(((Entry)sender), e.OldTextValue);
        }
    }
}
