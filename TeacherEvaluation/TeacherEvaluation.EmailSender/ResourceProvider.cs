using System.IO;
using System.Reflection;

namespace TeacherEvaluation.EmailSender
{
    public class ResourceProvider
    {
        public static string GetResourceText(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
