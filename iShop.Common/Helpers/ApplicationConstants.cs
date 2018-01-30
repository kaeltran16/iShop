namespace iShop.Common.Helpers
{
    public static class ApplicationConstants
    {
        public static class Error
        {
            public const string NotFound = "not_found";
            public const string SaveFailed = "save_failed";
            public const string InvalidFormat = "invalid_format";
            public const string NullOrEmpty = "null_or_empty";
            public const string InvalidSize = "invalid_size";
            public const string UnSupportedType = "unsupported_type";
            public const string Unauthorized = "unthorized";
        }

        public static class ControllerName
        {
            public const string Account = "Account";
            public const string Category = "Category";
            public const string Order = "Order";
            public const string Product = "Product";
            public const string ShoppingCart = "ShoppingCart";
            public const string Image = "Image";
            public const string Supplier = "Supplier";
            public const string Shipping = "Shipping";
        }
        public static class RoleName
        {
            public const string User = "User";
            public const string SuperUser = "SuperUser";
        }
        public static class PolicyName
        {
            public const string Users = "Users";
            public const string SuperUsers = "SuperUsers";
        }

        public static class ClaimName
        {
            public const string User = "User";
            public const string SuperUser = "SuperUser";
        }
    }
}
