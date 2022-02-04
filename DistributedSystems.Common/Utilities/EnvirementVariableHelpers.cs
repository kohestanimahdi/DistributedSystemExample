using System;

namespace DistributedSystems.Common.Utilities
{
    public static class EnvirementVariableHelpers
    {
        public static string GetValueAsString(string key, string defaultValue)
        {
            var env = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(env))
                env = defaultValue;

            return env;
        }
    }
}
