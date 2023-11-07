using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Mappers
{
    public interface IMapperModelToStr<TModel> 
    {
        public string ModelToString(TModel model);
        public TModel? StringToModel(string str);
    }
}
