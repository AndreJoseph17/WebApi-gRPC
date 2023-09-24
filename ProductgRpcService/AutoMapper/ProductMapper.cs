using AutoMapper;
using ProductgRpcService.Entities;
using ProductgRpcService.Protos;

namespace ProductgRpcService.AutoMapper
{
    public class ProductMapper: Profile
    {
        public ProductMapper() {
            CreateMap<Producto, ProductDetail>().ReverseMap();
        }
    }
}
