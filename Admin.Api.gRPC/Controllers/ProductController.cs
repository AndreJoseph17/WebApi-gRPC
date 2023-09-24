using Admin.Api.gRPC.Entities;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ProductgRpcService.Protos;
using System;

namespace Admin.Api.gRPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly ProductOfferService.ProductOfferServiceClient _client;
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel =
                GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:OfferServiceUrl"));
            _client = new ProductOfferService.ProductOfferServiceClient(_channel);
        }

        [HttpGet("getproductlist")]
        public async Task<Products> GetProductListAsync()
        {
            try
            {
                var response = await _client.GetProductListAsync(new Empty { });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpGet("getproductbyid")]
        public async Task<ProductDetail> GetProductByIdAsync(int Id)
        {
            try
            {
                var request = new GetProductDetailRequest
                {
                    ProductId = Id
                };

                var response = await _client.GetProductAsync(request);

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpPost("addproduct")]
        public async Task<ProductDetail> AddProductAsync(Producto producto)
        {
            try
            {
                var ProductDetail = new ProductDetail
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio.ToString(),
                };

                var response = await _client.CreateProductAsync(new CreateProductDetailRequest()
                {
                    Product = ProductDetail
                });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpPut("updateproduct")]
        public async Task<ProductDetail> UpdateProductAsync(Producto producto)
        {
            try
            {
                var productDetail = new ProductDetail
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio.ToString(),
                };

                var response = await _client.UpdateProductAsync(new UpdateProductDetailRequest()
                {
                    Product = productDetail
                });

                return response;
            }
            catch
            {

            }
            return null;
        }

        [HttpDelete("deleteproduct")]
        public async Task<DeleteProductDetailResponse> DeleteProductAsync(int Id)
        {
            try
            {
                var response = await _client.DeleteProductAsync(new DeleteProductDetailRequest()
                {
                    Id = Id
                });
                return response;
            }
            catch
            {

            }
            return null;
        }
    }
}
