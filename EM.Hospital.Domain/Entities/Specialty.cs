using EM.Hospital.Domain.Common;

namespace EM.Hospital.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Specialty()
        {
            
        }
        private Specialty(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public static Result<Specialty> Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Specialty>("Specialty name is required");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                return Result.Failure<Specialty>("Specialty description is required");
            }
            return Result.Success(new Specialty(name, description));
        }
        public void ActualizarEspecialidad(string name, string description)
        {
            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = Environment.UserName;
        }
    }
}
