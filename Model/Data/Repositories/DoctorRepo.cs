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
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HospitalContext _context;
        public DoctorRepo(HospitalContext context)
        {
            _context = context;
        }

        public ICollection<DoctorModel> GetDoctorModels()
        {
            ICollection<DoctorModel> doctorModels;
            try
            {
                ICollection<Doctor> listDoctor = _context.Doctors.Include(x => x.Type).ToList();

                doctorModels = new List<DoctorModel>();

                foreach (Doctor doctor in listDoctor)
                {
                    doctorModels.Add(new DoctorModel(doctor.Id, doctor.Name, doctor.Surname, new TypeDoctorModel(doctor.Type.Id, doctor.Type.Type)));
                }

                return doctorModels;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InitDoctors(ICollection<TypeDoctorModel> typeDoctorModels)
        {
            try
            {
                ICollection<TypeDoctor> typeDoctors = new List<TypeDoctor>();

                foreach (var model in typeDoctorModels)
                {
                    typeDoctors.Add(ConverterModelToEF.Convert(model));
                }

                ICollection<Doctor> doctors = new List<Doctor>()
                {
                    new Doctor()
                    {
                        Name = "Иван",
                        Surname = "Иванов",
                        TypeId = typeDoctors.FirstOrDefault(t => t.Type == "Терапевт").Id
                    },
                    new Doctor()
                    {
                        Name = "Михаил",
                        Surname = "Михайлов",
                        TypeId = typeDoctors.FirstOrDefault(t => t.Type == "Дерматолог").Id
                    },
                    new Doctor()
                    {
                        Name = "Петр",
                        Surname = "Петров",
                        TypeId = typeDoctors.FirstOrDefault(t => t.Type == "ЛОР").Id
                    },
                    new Doctor()
                    {
                        Name = "Сидр",
                        Surname = "Сидоров",
                        TypeId = typeDoctors.FirstOrDefault(t => t.Type == "Психолог").Id
                    },
                    new Doctor()
                    {
                        Name = "Александр",
                        Surname = "Александров",
                        TypeId = typeDoctors.FirstOrDefault(t => t.Type == "Гастроэнтеролог").Id
                    }
                };
                _context.ChangeTracker.Clear();
                _context.Doctors.AddRange(doctors);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
