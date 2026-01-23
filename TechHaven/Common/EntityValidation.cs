namespace TechHaven.Common;

public static class EntityValidation
{
    public static class Product
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 1000;
        public const int SpecsJsonMaxLength = 2000;
        public const int ImageUrlMaxLength = 500;
    }

    public static class Category
    {
        public const int NameMaxLength = 50;
    }

    public static class ApplicationUser
    {
        public const int DisplayNameMaxLength = 50;
    }
}
