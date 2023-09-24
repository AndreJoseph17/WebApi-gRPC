using AutoMapper;
using Grpc.Core;
using ProductgRpcService.Entities;
using ProductgRpcService.Protos;
using ProductgRpcService.Repositories;
using ProductOfferService = ProductgRpcService.Protos.ProductOfferService;

namespace ProductgRpcService.Services
{
    public class ProductService : ProductOfferService.ProductOfferServiceBase
    {
        private readonly IProductOfferService _prductOfferService;
        private readonly IMapper _mapper;

        public ProductService(IProductOfferService prductOfferService, IMapper mapper)
        {
            _prductOfferService = prductOfferService;
            _mapper = mapper;
        }

        public async override Task<Products> GetProductList(Empty request, ServerCallContext context)
        {
            var productsData = await _prductOfferService.GetProductListAsync();

            Products response = new Products();
            foreach (Producto product in productsData)
            {
                response.Items.Add(_mapper.Map<ProductDetail>(product));
            }

            return response;
        }

        public async override Task<ProductDetail> GetProduct(GetProductDetailRequest request, ServerCallContext context)
        {
            var product = await _prductOfferService.GetProductByIdAsync(request.ProductId);
            var productDetail = _mapper.Map<ProductDetail>(product);
            return productDetail;
        }

        public async override Task<ProductDetail> CreateProduct(CreateProductDetailRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Producto>(request.Product);

            await _prductOfferService.AddProductAsync(product);

            var productDetail = _mapper.Map<ProductDetail>(product);
            return productDetail;
        }

        public async override Task<ProductDetail> UpdateProduct(UpdateProductDetailRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Producto>(request.Product);

            await _prductOfferService.UpdateProductAsync(product);

            var productDetail = _mapper.Map<ProductDetail>(product);
            return productDetail;
        }

        public async override Task<DeleteProductDetailResponse> DeleteProduct(DeleteProductDetailRequest request, ServerCallContext context)
        {
            var isDeleted = await _prductOfferService.DeleteProductAsync(request.Id);
            var response = new DeleteProductDetailResponse
            {
                IsDelete = isDeleted
            };

            return response;
        }
    }
}
