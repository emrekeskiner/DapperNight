using Dapper;
using DapperNight.Context;
using DapperNight.Dtos.ProductDtos;

namespace DapperNight.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DapperContext _context;

        public ProductService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into TblProduct (ProductName,Stock,Price,CategoryId) values (@productName,@stock,@price,@categoryId)";
            var parameters = new DynamicParameters();
            parameters.Add("@productName",createProductDto.ProductName);
            parameters.Add("@stock", createProductDto.Stock);
            parameters.Add("@price", createProductDto.Price);
            parameters.Add("@categoryId", createProductDto.CategoryId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteProductAsync(int id)
        {
            string query = "Delete From TblProduct Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select ProductId,ProductName,CategoryName,Stock,Price From TblProduct inner join TblCategory on TblProduct.CategoryId=TblCategory.CategoryId";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultProductDto>(query);
            return values.ToList();
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(int id)
        {
            string query = "Select ProductId, ProductName,TblProduct.CategoryId,CategoryName,Stock,Price From TblProduct inner join TblCategory on TblProduct.CategoryId=TblCategory.CategoryId Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            var connection = _context.CreateConnection();
            var value = await connection.QueryFirstOrDefaultAsync<GetByIdProductDto>(query,parameters);
            return value;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            string query = "Update TblProduct Set ProductName=@productName,Stock=@stock,Price=@price,CategoryId=@categoryId Where ProductId=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productName", updateProductDto.ProductName);
            parameters.Add("@stock", updateProductDto.Stock);
            parameters.Add("@price", updateProductDto.Price);
            parameters.Add("@categoryId", updateProductDto.CategoryId);
            parameters.Add("@productId", updateProductDto.ProductId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }


    }
}
