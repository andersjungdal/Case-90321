namespace Billedstyring.Profiles
{
    public class BilledstyringProfile : AutoMapper.Profile
    {
        public BilledstyringProfile()
        {
            CreateMap<Db.Billedstyring, Models.Billedstyring>();
            CreateMap<Models.Billedstyring, Db.Billedstyring>();
        }
    }
}
