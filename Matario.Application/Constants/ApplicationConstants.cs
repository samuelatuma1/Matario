using System;
namespace Matario.Application.Constants
{
	public static partial class ApplicationConstants
	{
		public static class TimeConstants
		{
			public const int MinutesInAnHour = 60;
			public const int SecondsInAMinute = 60;
            public const int HoursInADay = 24;
        }

		public static class ClaimConstants
		{
            public const string PermissionsClaimType = "Permissions";
            public const string RolesClaimType = "Roles";
            public const string IdClaimType = "Id";
        }
	}

    public static partial class ApplicationConstants
    {
        public static class AuthenticationConstants
        {
            public const string DefaultUserPassword = "Password123$";
        }
    }

    public static partial class ApplicationConstants
    {
        public static class PermissionConstants
        {
            public const string AdminCreate = "AdminCreate";
            public const string CreateManager = "Create Manager";
        }

        public static class RoleConstants
        {
            public const string Admin = "Admin";
            public const string CorporateAdmin = "CorporateAdmin";
            public const string Manager = "Manager";
        }
    }
}

