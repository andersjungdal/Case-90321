namespace Kundestyring.Profiles
{
    public class KundestyringProfile : AutoMapper.Profile
    {
        public KundestyringProfile()
        {
            CreateMap<Db.Kundestyring, Models.Kundestyring>();
            CreateMap<Models.Kundestyring, Db.Kundestyring>();
        }
    }
}
