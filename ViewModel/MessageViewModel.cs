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
    public class MessageViewModel : ReactiveObject
    {
		private string _message;
        private static MessageViewModel _instance;
        public static MessageViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(MessageViewModel))
                    {
                        if (_instance == null)
                        {
                            _instance = new MessageViewModel();
                        }
                    }
                }
                return _instance;
            }
        }
        public string Message
		{
			get { return _message; }
			set { this.RaiseAndSetIfChanged(ref _message, value); }
		}

        private string _typeMessage;

        public string TypeMessage
        {
            get { return _typeMessage; }
            set { this.RaiseAndSetIfChanged(ref _typeMessage, value); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { this.RaiseAndSetIfChanged(ref isVisible, value); }
        }

        public ReactiveCommand<Unit, Unit> ChangeVisibilityViewCommand { get; }

        private MessageViewModel()
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
    }
}
