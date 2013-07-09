using System;
using System.IO;
using System.Reflection;

namespace LogitechInterface
{
    internal class DynamicLoading
    {
        /// <summary>
        ///     Enables dynamic loading of DLL's from embeded resources
        /// </summary>
        /// <param name="prefix">
        ///     A string to add to the begining for namespace names. Should be in the format
        ///     <c>"NamespaceName"</c>
        /// </param>
        /// <param name="x64Extension">
        ///     An extension to insert between the resource name and the file extension
        ///     if the process is running in 64-bit mode. Input a blank string if not needed.
        /// </param>
        public static void EnableDynamicLoading(string prefix, string x64Extension)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    string pfix = prefix == "" ? "" : prefix + ".";
                    string extension = IntPtr.Size == 8 ? x64Extension : "";
                    string resName = pfix + args.Name + extension + ".dll";
                    Assembly thisAssembly = Assembly.GetExecutingAssembly();
                    using (Stream input = thisAssembly.GetManifestResourceStream(resName))
                    {
                        return input != null
                                   ? Assembly.Load(StreamToBytes(input))
                                   : null;
                    }
                };
        }

        private static byte[] StreamToBytes(Stream input)
        {
            int capacity = input.CanSeek ? (int) input.Length : 0;
            using (var output = new MemoryStream(capacity))
            {
                int readLength;
                var buffer = new byte[4096];

                do
                {
                    readLength = input.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, readLength);
                } while (readLength != 0);

                return output.ToArray();
            }
        }
    }
}