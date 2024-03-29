﻿namespace BoardRoomSystem.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static class Policies
        {
            public const string RequireSuperAdmin = "RequireSuperAdmin";
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireUser = "RequireUser";
        }
    }
}
