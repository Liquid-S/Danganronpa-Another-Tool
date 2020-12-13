using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danganronpa_Another_Tool
{
    public static class CPK
    {
        public static void ExtractCPK(string CPK, string DestinationDir)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            string CodeLine = "-X -i \"" + CPK + "\" -o \"" + DestinationDir + "\"";

            Images.UseEXEToConvert("Ext\\YACpkTool.exe", CodeLine);
        }

        public static void RepackCPK(string FolderToRebuildToWAD, string DestinationDir)
        {

        }
    }
}
