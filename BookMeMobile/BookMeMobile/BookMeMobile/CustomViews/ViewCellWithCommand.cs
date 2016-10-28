using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.ViewModels.Concrete.Reservations;
using Xamarin.Forms;

namespace BookMeMobile.CustomViews
{
    public class ViewCellWithCommand : ViewCell
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create<ViewCellWithCommand, ICommand>(x => x.Command, null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<ViewCellWithCommand, ReservationViewModel>(x => x.CommandParameter, null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public ReservationViewModel CommandParameter
        {
            get { return (ReservationViewModel)GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public ViewCellWithCommand()
        {
            this.Tapped += (sender, args) =>
            {
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(this.CommandParameter);
                }
            };
        }
    }
}