using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel;

namespace View.Commands
{
    abstract class AbstractCommand : ICommand
    {
        protected HospitalViewModel _hospitalVeiwModel;

        public AbstractCommand(HospitalViewModel mainWindowVeiwModel)
        {
            _hospitalVeiwModel = mainWindowVeiwModel;
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
