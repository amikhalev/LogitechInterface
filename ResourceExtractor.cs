using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LogitechInterface
{
    /// <summary>
    /// A class for extracting embeded resources
    /// </summary>
    public static class ResourceExtractor
    {
        /// <summary>
        /// Extracts a resource "resourceName" into "fileName"
        /// </summary>
        /// <param name="resourceName">The resource name to extract</param>
        /// <param name="fileName">The file name to extract to</param>
        public static void ExtractResourceToFile(string resourceName, string fileName)
        {
            if (!File.Exists(fileName))
                using (var embededFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                using (var newFile = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    if (embededFile == null)
                        throw new DllNotFoundException("Embeded DLL " + resourceName + " not found.");
                    var b = new byte[embededFile.Length];
                    embededFile.Read(b, 0, b.Length);
                    newFile.Write(b, 0, b.Length);
                }
        }
    }
}
