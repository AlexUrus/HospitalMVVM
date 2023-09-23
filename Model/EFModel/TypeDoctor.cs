using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model.EFModel
{
    public class TypeDoctor
    {
        public string Type {  get; set; }

        public override string ToString()
        {
            return Type;
        }
        /*
        [Description("Терапевт")]
        Therapist = 1,
        [Description("Дерматолог")]
        Dermatologist = 2,
        [Description("ЛОР")]
        Otolaryngologist = 3,
        [Description("Психолог")]
        Psychologist = 4,
        [Description("Гастроэнтеролог")]
        Gastroenterologist = 5
        */
    }
}
