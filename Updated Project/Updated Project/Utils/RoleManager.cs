using System;

namespace PatientVisitManager.Utils
{
    public enum UserRole { Admin, Receptionist }

    public static class RoleManager
    {
        public static UserRole? Login()
        {
            Console.Write("Username: ");
            var u = Console.ReadLine();
            Console.Write("Password: ");
            var p = Console.ReadLine();

            return (u, p) switch
            {
                ("admin", "1234") => UserRole.Admin,
                ("reception", "1234") => UserRole.Receptionist,
                _ => null
            };
        }
    }
}
