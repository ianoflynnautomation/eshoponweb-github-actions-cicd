using AutoMapper;
using Microsoft.Playwright;

namespace Playwright.DotNet.SyncPlaywright.GetByOptions;

internal static class MapperService
{
        private static MapperConfiguration Config;
        private static IMapper Mapper;
        static MapperService()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetByAltTextOptions, LocatorGetByAltTextOptions>();
                cfg.CreateMap<GetByAltTextOptions, FrameLocatorGetByAltTextOptions>();
                cfg.CreateMap<GetByAltTextOptions, PageGetByAltTextOptions>();

                cfg.CreateMap<GetByLabelOptions, LocatorGetByLabelOptions>();
                cfg.CreateMap<GetByLabelOptions, FrameLocatorGetByLabelOptions>();
                cfg.CreateMap<GetByLabelOptions, PageGetByLabelOptions>();

                cfg.CreateMap<GetByPlaceholderOptions, LocatorGetByPlaceholderOptions>();
                cfg.CreateMap<GetByPlaceholderOptions, FrameLocatorGetByPlaceholderOptions>();
                cfg.CreateMap<GetByPlaceholderOptions, PageGetByPlaceholderOptions>();

                cfg.CreateMap<GetByRoleOptions, LocatorGetByRoleOptions>();
                cfg.CreateMap<GetByRoleOptions, FrameLocatorGetByRoleOptions>();
                cfg.CreateMap<GetByRoleOptions, PageGetByRoleOptions>();

                cfg.CreateMap<GetByTextOptions, LocatorGetByTextOptions>();
                cfg.CreateMap<GetByTextOptions, FrameLocatorGetByTextOptions>();
                cfg.CreateMap<GetByTextOptions, PageGetByTextOptions>();

                cfg.CreateMap<GetByTitleOptions, LocatorGetByTitleOptions>();
                cfg.CreateMap<GetByTitleOptions, FrameLocatorGetByTitleOptions>();
                cfg.CreateMap<GetByTitleOptions, PageGetByTitleOptions>();
            });

            Mapper = Config.CreateMapper();
        }

        public static T ConvertTo<T>(this IOptions options)
        {
            return Mapper.Map<T>(options);
        }
}
