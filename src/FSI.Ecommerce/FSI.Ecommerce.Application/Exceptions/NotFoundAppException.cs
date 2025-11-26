namespace FSI.Ecommerce.Application.Exceptions
{
    public sealed class NotFoundAppException : ApplicationExceptionBase
    {
        public string ResourceName { get; }
        public object? Key { get; }

        public NotFoundAppException(string resourceName, object? key = null)
            : base(BuildMessage(resourceName, key))
        {
            ResourceName = resourceName;
            Key = key;
        }

        private static string BuildMessage(string resourceName, object? key)
        {
            return key is null
                ? $"{resourceName} was not found."
                : $"{resourceName} with key '{key}' was not found.";
        }
    }
}