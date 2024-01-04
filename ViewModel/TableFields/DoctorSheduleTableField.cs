using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Models
{
    public class DoctorSheduleTableField
    {
        [DisplayName("")]
        public int Id { private get; set; }

        [DisplayName("Имя")]
        public string Имя { get; set; }

        [DisplayName("Фамилия")]
        public string Фамилия { get; set; }

        [DisplayName("Часы приема")]
        public string Часы_приема { get; set; }
    }
}
