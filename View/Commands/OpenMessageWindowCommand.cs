using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View;
using View.Commands;

namespace ViewModel.Commands
{
    class OpenMessageWindowCommand : AbstractCommand
    {
        public OpenMessageWindowCommand(HospitalViewModel hospitalVeiwModel) : base(hospitalVeiwModel)
        {
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            var displayRootRegistry = (Application.Current as App).DisplayRootRegistry;

            var dialogWindowViewModel = new MessageViewModel();
            await displayRootRegistry.ShowModalPresentation(dialogWindowViewModel);

        }
    }
}
