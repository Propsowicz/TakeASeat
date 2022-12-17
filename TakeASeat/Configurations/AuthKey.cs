namespace TakeASeat.Configurations
{
    public class AuthKey
    {
        private static string appKey = "some-long-secret-key-1234567890?><!@#$%";
        public static string AppKey { get { return appKey; } }

    }
}
