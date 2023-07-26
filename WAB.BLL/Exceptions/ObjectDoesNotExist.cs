namespace WAB.BLL.Exceptions;

public class ObjectDoesNotExistException : Exception
{
    public ObjectDoesNotExistException(string message) : base(message)
    {
    }
}