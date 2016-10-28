using System;
using System.Linq;
using BookMeMobile.Render;
using BookMeMobile.ViewModels.Concrete;

namespace BookMeMobile.Pages
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            this.InitializeComponent();
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
            this.Picker.SelectedIndexChanged += this.Picker_SelectedIndexChanged;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var picker = (BindablePicker)sender;
                ((ProfileViewModel)this.ViewModel).FavoriteRoom = picker.ItemsSource[picker.SelectedIndex].ToString();
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}