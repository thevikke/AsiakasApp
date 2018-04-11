using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsiakasApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(AsiakasContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
