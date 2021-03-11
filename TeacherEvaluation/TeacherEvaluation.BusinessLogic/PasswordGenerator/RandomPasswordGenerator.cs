using System;

namespace TeacherEvaluation.BusinessLogic.PasswordGenerator
{
    class RandomPasswordGenerator
    {
        public static string GeneratePassword(int passwordSize)
        {
            char[] password = new char[passwordSize];
            Random random = new Random();

            string charSet = "abcdefghijklmnopqursuvwxyz" +
                             "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                             "123456789" +
                              @"!@$%^&*()#";

            for (int contor = 0; contor < passwordSize; contor++)
            {
                password[contor] = charSet[random.Next(charSet.Length - 1)];
            }

            return string.Join(null, password);
        }
    }
}
