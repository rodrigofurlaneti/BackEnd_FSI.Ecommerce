namespace FSI.Ecommerce.Application.Exceptions
{
    public sealed class ValidationAppException : ApplicationExceptionBase
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationAppException(
            string message,
            IReadOnlyDictionary<string, string[]> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}