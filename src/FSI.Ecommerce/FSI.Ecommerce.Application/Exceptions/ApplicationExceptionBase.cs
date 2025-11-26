namespace FSI.Ecommerce.Application.Exceptions
{
    /// <summary>
    /// Base type for all application-layer exceptions.
    /// Do not expose these directly to end users; map them to proper HTTP responses.
    /// </summary>
    public abstract class ApplicationExceptionBase : Exception
    {
        protected ApplicationExceptionBase(string message)
            : base(message)
        {
        }

        protected ApplicationExceptionBase(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}