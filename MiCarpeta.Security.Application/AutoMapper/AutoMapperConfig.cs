using AutoMapper;
using MiCarpeta.Security.Common;
using MiCarpeta.Security.Domain.Entities;

namespace MiCarpeta.Security.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static readonly object ThisLock = new object();

        public static void Initialize()
        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock (ThisLock)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Response, ResponseViewModel>()
                        .ReverseMap();
                });

            }
        }
    }
}
