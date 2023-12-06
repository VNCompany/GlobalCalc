using System.IO;

namespace GlobalCalc.UI.Helpers
{
    internal static class ImagesHelper
    {
        public static void LoadImageFromStream(Stream stream, string outputFile)
        {
            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                byte[] buf = new byte[4096];
                int count;
                while ((count = stream.Read(buf, 0, buf.Length)) != 0)
                    fs.Write(buf, 0, count);

                fs.Flush();
            }
        }
    }
}
