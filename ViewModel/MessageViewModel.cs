using Model.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MessageViewModel : BaseViewModel
    {
        private string _message; 
        private string _typeMessage;
        private bool isVisible;

        public string Message
		{
			get { return _message; }
			set { this.RaiseAndSetIfChanged(ref _message, value); }
		}

        public string TypeMessage
        {
            get { return _typeMessage; }
            set { this.RaiseAndSetIfChanged(ref _typeMessage, value); }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { this.RaiseAndSetIfChanged(ref isVisible, value); }
        }

        public ReactiveCommand<Unit, Unit> ChangeVisibilityViewCommand { get; }

        public MessageViewModel()
        {
            IsVisible = false;
            ChangeVisibilityViewCommand = ReactiveCommand.Create(ChangeVisibilityView);
        }

        private void ChangeVisibilityView()
        {
            if (IsVisible)
                IsVisible = false;
            else
                IsVisible = true;
        }

        public void ShowMessage(string message, string type)
        {
            Message = message;
            TypeMessage = type;
            IsVisible = true;
        }
    }
}
