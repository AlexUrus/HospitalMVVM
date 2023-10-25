using Model.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BaseViewModel : ReactiveObject
    {
        protected static Dictionary<string, AbstractModel> stringModelDictionary;
        protected static AbstractModel GetModel(string modelString)
        {
            AbstractModel model;
            stringModelDictionary.TryGetValue(modelString, out model);
            return model;
        }

        public BaseViewModel()
        {
            stringModelDictionary = new Dictionary<string, AbstractModel>();
        }
    }
}
