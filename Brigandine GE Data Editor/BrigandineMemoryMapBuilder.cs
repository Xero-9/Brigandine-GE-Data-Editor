using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;

namespace BrigandineGEDataEditor
{
    public static class BrigandineMemoryMapBuilder
    {
        public static MemoryMappedFile BrigandineAsMemoryMappedFile { get; private set; }

        static BrigandineMemoryMapBuilder()
        {
            BuildMemoryMapFromResourceFile();
        }

        private static void BuildMemoryMapFromResourceFile()
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

                BrigandineAsMemoryMappedFile = memoryMappedFile;
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