namespace Example.IntegrationTests._SetUp
{
    public static class ApiRoute
    {
        public static string Base { get; private set; }

        public static void SetBase(string routeBase)
        {
            Base = routeBase;
        }
    }
}
