﻿namespace Shopping.web.Models.Catalog
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }

    //Wrapper classes

    public record GetProductsResponse(IEnumerable<ProductModel> Products);
    public record GetProductsByIdResponse(ProductModel Product);
    public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
}
