namespace Varelagerstyring.Profiles
{
    public class VarelagerstyringProfile : AutoMapper.Profile
    {
        public VarelagerstyringProfile()
        {
            CreateMap<Varelagerstyring.Db.Varelagerstyring, Varelagerstyring.Models.Varelagerstyring>();
            CreateMap<Varelagerstyring.Models.Varelagerstyring, Varelagerstyring.Db.Varelagerstyring>();
        }
    }
}
