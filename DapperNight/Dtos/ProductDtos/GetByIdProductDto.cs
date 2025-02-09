﻿namespace DapperNight.Dtos.ProductDtos;

public class GetByIdProductDto
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}
