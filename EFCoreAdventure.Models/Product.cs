namespace EFCoreAdventure.Models
{
    public class Product : Entity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }

        public int BrandId { get; set; }

        public  ProductBrand Brand { get; set; }

        // Quantity in stock
        public int AvailableStock { get; set; }

        public int MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }

    }
}
