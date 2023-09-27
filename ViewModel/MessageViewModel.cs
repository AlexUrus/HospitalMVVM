using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MessageViewModel : ReactiveObject
    {
		private string _message;

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
    }
}
