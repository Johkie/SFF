using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFF_API
{
    public static class ApiRoutes
    {
        public static class RouteBase
        {
            public const string Root = "api";

            public static class Movies
            {
                private const string Base = Root + "/movies";
                public const string GetAll = Base + "/{id}";
            }
        }

    }
}
