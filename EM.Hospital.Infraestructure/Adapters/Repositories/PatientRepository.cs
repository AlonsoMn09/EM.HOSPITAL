using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Domain.Entities;
using EM.Hospital.Infraestructure.Configuration.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Adapters.Repositories
{
    public class PatientRepository(HospitalDbContext context) : BaseRepository<Patient>(context), IPatientRepository
    {
    }
}
