using System;
namespace Matario.Constants
{
	public static class ApiConstant
	{
		public static class RouteConstants
		{
			public const string OrganisationBaseRoute = "api/v1/tenant/{organisationName}";
			public const string OrganisationNameKeyInRoute = "organisationName";

            public const string SuperAdminBaseRoute = "api/v1/superadmin";
		}
	}
}

