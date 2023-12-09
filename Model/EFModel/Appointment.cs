using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.EFModel
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public Patient Patient { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public AppointmentTime AppointmentTime { get; set; }

        [ForeignKey("AppointmentTime")]
        public int AppointmentTimeId { get; set; }
    }
}
