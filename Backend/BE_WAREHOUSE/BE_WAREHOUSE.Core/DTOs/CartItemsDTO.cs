using BE_WAREHOUSE.Core.Entities;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class CartItemsDTO
    {
        public Guid CartItemsId { get; set; }
        public int Quantity { get; set; }
        public Guid CartsId { get; set; }
        public Guid UsersId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoriesId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBrandName { get; set; }
        public Double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSlug { get; set; }
        public Boolean ProductNewFlag { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string? CategoriesName { get; set; }
        public string? CategoriesSlug { get; set; }
        public string? CategoriesDescription { get; set; }
        public string? CategoriesCode { get; set; }
        public Guid ImagesId { get; set; }
        public string ImagesPath { get; set; }
    }
}
