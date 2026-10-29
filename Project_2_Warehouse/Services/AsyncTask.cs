using System;
using System.Threading.Tasks;

namespace Project_2_Warehouse.Services
{
    public static class AsyncTask
    {
        public static Task<T> Run<T>(Func<T> operation)
        {
            return Task.Run(operation);
        }

        public static Task Run(Action operation)
        {
            return Task.Run(operation);
        }
    }
}
