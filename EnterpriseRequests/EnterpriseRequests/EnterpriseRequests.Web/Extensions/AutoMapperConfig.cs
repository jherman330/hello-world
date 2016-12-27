using AutoMapper;
//using EnterpriseRequests.Web.Extensions.ObjectConversions; //TODO: uncomment using statement

namespace EnterpriseRequests.Web.Extensions
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Config;

        public static IMapper Mapper;

        public static void CreateMaps()
        {
            Config = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile<EntityProfile>();
            });

            Mapper = Config.CreateMapper();
        }
    }
}