namespace TakeASeat.ProgramConfigurations.DTO
{
    public class ElementIsUsageException : Exception
    {
        public ElementIsUsageException(string Message) : base (Message)
        {

        }
                
    }

    public class CantAccessDataException : Exception
    {
        public CantAccessDataException(string Message) : base(Message)
        {

        }

    }
}
