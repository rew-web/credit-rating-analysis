using System;

namespace CreditRatingApp
{
    public static class DbConfig
    {
        public static string ConnectionString
        {
            get
            {
                string connectionString =
                    Environment.GetEnvironmentVariable(
                        "CREDIT_RATING_DB_CONNECTION_STRING");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        "Не задана строка подключения к PostgreSQL.");
                }

                return connectionString;
            }
        }
    }
}