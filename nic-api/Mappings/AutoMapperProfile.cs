using AutoMapper;
using nic_api.Domain;
using nic_api.Models;

namespace nic_api.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Doctor, IndexDoctor>();
            CreateMap<Doctor, UpdateDoctor>();
            CreateMap<CreateDoctor, Doctor>();
            CreateMap<UpdateDoctor, Doctor>();            

            CreateMap<Patient, IndexPatient>();
            CreateMap<Patient, UpdatePatient>();
            CreateMap<CreatePatient, Patient>();
            CreateMap<UpdatePatient, Patient>();

            CreateMap<Area, IndexArea>();
            CreateMap<Office, IndexOffice>();            
            CreateMap<Specialization, IndexSpecialization>();
        }
    }
}
