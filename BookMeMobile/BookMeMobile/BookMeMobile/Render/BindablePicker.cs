using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Infrastructure;
using Xamarin.Forms;

namespace BookMeMobile.Render
{
    public class BindablePicker : Picker
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<BindablePicker, IEnumerable>(p => p.ItemsSource, null, propertyChanged: OnItemsSourcePropertyChanged);

        public static readonly BindableProperty SelectedItemProperty =
             BindableProperty.Create<BindablePicker, object>(p => p.SelectedItem, null, BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable value, IEnumerable newValue)
        {
            var picker = (BindablePicker)bindable;
            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {
                            picker.Items.Add((newItem ?? string.Empty).ToString());
                        }
                    }

                    if (args.OldItems != null)
                    {
                        foreach (var oldItem in args.OldItems)
                        {
                            picker.Items.Remove((oldItem ?? string.Empty).ToString());
                        }
                    }
                };
            }

            if (newValue == null)
            {
                return;
            }

            picker.Items.Clear();

            foreach (var item in newValue)
            {
                picker.Items.Add((item ?? string.Empty).ToString());
            }
        }

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var picker = (BindablePicker)bindable;
            if (picker.ItemsSource != null)
            {
                picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
            }
        }
    }
}