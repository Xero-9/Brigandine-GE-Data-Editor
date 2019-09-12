using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;

namespace BrigandineGEDataEditor
{
    internal static class DefaultMemoryMappedFileBuilder
    {
        /// <summary>
        /// Builds a MemoryMappedFile for the DefaultData resource.
        /// </summary>
        /// <returns>A MemoryMappedFile for the DefaultData resource.</returns>
        public static MemoryMappedFile BuildMemoryMapFromResourceFile()
        {
            using (var stream = GetResourceStream("DefaultData"))
            {
                var memoryMappedFile =
                    MemoryMappedFile.CreateOrOpen("DefaultData", stream.Length);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int) stream.Length);

                using (var va = memoryMappedFile.CreateViewAccessor())
                {
                    va.WriteArray(0, bytes, 0, (int) stream.Length);
                    va.Flush();
                }

                return memoryMappedFile;
            }
        }

        private static UnmanagedMemoryStream GetResourceStream(String name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string[] resources = assembly.GetManifestResourceNames();
            string resource =
                resources.SingleOrDefault(r => r.EndsWith(name, StringComparison.CurrentCultureIgnoreCase));

            if (resource == null)
            {
                throw new ArgumentException("The resource does not exist.",
                    "name");
            }

            return (UnmanagedMemoryStream) assembly.GetManifestResourceStream(resource);
        }
    }
}