using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Data.Interfaces;
using Model.EFModel;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.Repositories
{
    public class TypeDoctorRepo : ITypeDoctorRepo
    {
        private readonly HospitalContext _context;
        public TypeDoctorRepo(HospitalContext context)
        {
            _context = context;
        }
        public ICollection<TypeDoctorModel> GetTypeDoctorModels()
        {
            ICollection<TypeDoctorModel> typeDoctorModels;
            try
            {
                ICollection<TypeDoctor> listTypeDoctors = _context.TypeDoctors.ToList();
                typeDoctorModels = new List<TypeDoctorModel>();

                foreach (TypeDoctor typeDoctor in listTypeDoctors)
                {
                    typeDoctorModels.Add(new TypeDoctorModel(typeDoctor.Id, typeDoctor.Type));
                }

                return typeDoctorModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InitTypeDoctorModels()
        {
            try
            {
                List<TypeDoctor> typeDoctors = new List<TypeDoctor>()
                {
                new TypeDoctor()
                {
                    Type = "Терапевт"
                },
                new TypeDoctor()
                {
                    Type = "Дерматолог"
                },
                new TypeDoctor()
                {
                    Type = "ЛОР"
                },
                new TypeDoctor()
                {
                    Type = "Психолог"
                },
                new TypeDoctor()
                {
                    Type = "Гастроэнтеролог"
                },
                };

                _context.TypeDoctors.AddRange(typeDoctors);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
