using System;

namespace Parser.Enums
{
    public static class OperationsTools
    {
        public static Operations ToOperations(string operation)
        {
            switch (operation)
            {
                case "write":
                    return Operations.write;
                case "read":
                    return Operations.read;
                case "assign":
                case "=":
                    return Operations.assign;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown operation '{operation}'");
            }
        }
    }
}