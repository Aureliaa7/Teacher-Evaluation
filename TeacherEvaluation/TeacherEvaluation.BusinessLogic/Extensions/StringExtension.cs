namespace TeacherEvaluation.BusinessLogic.Extensions
{
    static class StringExtension
    {
        public static string ConvertFirstLetterToUpperCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            char[] characters = str.ToCharArray();
            characters[0] = char.ToUpper(characters[0]);
            return new string(characters);
        }
    }
}
