
namespace Model.Model
{
    public class PatientModel : AbstractModel
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Surname { get; init; }

        public PatientModel(int id, string name, string surname) 
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

    }
}
