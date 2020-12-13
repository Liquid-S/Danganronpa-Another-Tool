using System;
using System.IO;
using System.Text;

namespace Danganronpa_Another_Tool
{
    public static class DATpsp
    {
        public static void ExtractDAT(string IndirizzoUMDIMAGE, string DestinationDir, long pos, string FILEBOOT, string GameVersion)
        {
            using (FileStream FILEDAT = new FileStream(IndirizzoUMDIMAGE, FileMode.Open, FileAccess.Read), EBOOT = new FileStream(FILEBOOT, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader LettoreDAT = new BinaryReader(FILEDAT, Encoding.Default), LettoreEBOOT = new BinaryReader(EBOOT))
                {
                    uint AmountOfFiles = 0;

                    if (GameVersion == "FULL") // If the user is working with the full game files.
                        AmountOfFiles = LettoreDAT.ReadUInt32(); // Reads the number of files inside the UMDIMAGE.DAT.
                    else // If the user has chosen to work with the demo files.
                    {
                        EBOOT.Seek(pos, SeekOrigin.Begin);
                        AmountOfFiles = LettoreEBOOT.ReadUInt32(); // Reads the number of files inside the UMDIMAGE.DAT.
                        LettoreEBOOT.ReadUInt32(); // Unknown and "useless".
                    }

                    string[] FilesNames = new string[AmountOfFiles];
                    uint[] OffsetFiles = new uint[AmountOfFiles], FilesSizes = new uint[AmountOfFiles];

                    if (GameVersion == "FULL") // If the user is working with the full game files.
                    {
                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            FILEDAT.Seek(i * 0x4 + 0x4, SeekOrigin.Begin); // Move at the beginning of the info of each file inside the DAT. 
                            OffsetFiles[i] = LettoreDAT.ReadUInt32(); // Saves the file offset involved.
                            FilesSizes[i] = LettoreDAT.ReadUInt32(); // Saves the offset of the next file in order to calculate the size of the current file.

                            if (FilesSizes[i] == 0) // If the size of the next file is 0, it means that it's the very latest file.
                                FilesSizes[i] = (uint)FILEDAT.Length - OffsetFiles[i]; // So the file size will be obtained taking into account the end of the DAT.
                            else
                                FilesSizes[i] -= OffsetFiles[i]; // File size = starting point of the next file - starting point of the current file. 

                            EBOOT.Seek(pos, SeekOrigin.Begin); // Moves at the beginning of the offset of the filename.
                            uint Puntatore = LettoreEBOOT.ReadUInt32() + 0xC0; // Saves the offset of the filename.
                            pos = EBOOT.Position + 8; /* pos now points to the offset of the next filename.
                            The +8 is due to the fact that in the between an offset and the next there is some "unnecessary" data. */

                            EBOOT.Seek(Puntatore, SeekOrigin.Begin); // Move at the beginning of the filename.

                            // START Read and save the filename.
                            byte Fine = LettoreEBOOT.ReadByte();
                            while (Fine != 0)
                            {
                                FilesNames[i] += (char)Fine;
                                Fine = LettoreEBOOT.ReadByte();
                            } //END Read and save the filename.
                        }

                        Array.Sort(FilesNames, new DRAT.AlphanumComparatorFast()); // Order filenames alphanumetically.
                    }
                    else // If the user is working with the demo files.
                    {
                        uint[] OffsetNamesfiles = new uint[AmountOfFiles];

                        // Reads from the eboot the offsets of the file names and body. It also reads the size of each file.
                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            OffsetNamesfiles[i] = LettoreEBOOT.ReadUInt32() + 0xC0;
                            OffsetFiles[i] = LettoreEBOOT.ReadUInt32();
                            FilesSizes[i] = LettoreEBOOT.ReadUInt32();
                        }

                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            EBOOT.Seek(OffsetNamesfiles[i], SeekOrigin.Begin); // Move at the beginning of the filename.

                            // START Read and save the filename.
                            byte Fine = LettoreEBOOT.ReadByte();
                            while (Fine != 0)
                            {
                                FilesNames[i] += (char)Fine;
                                Fine = LettoreEBOOT.ReadByte();
                            } //END Read and save the filename.
                        }
                    }

                    for (int i = 0; i < AmountOfFiles; i++) // Files extraction start now.
                    {
                        FILEDAT.Seek(OffsetFiles[i], SeekOrigin.Begin); // Move at the beginning of the body's file within the file.DAT.

                        byte[] CorpoDelFile = new byte[FilesSizes[i]];
                        FILEDAT.Read(CorpoDelFile, 0, CorpoDelFile.Length); // Save the body's file.

                        using (FileStream Extract = new FileStream(Path.Combine(DestinationDir, FilesNames[i]), FileMode.Create, FileAccess.Write))
                            Extract.Write(CorpoDelFile, 0, CorpoDelFile.Length); // Makes and saves the new file.
                    }
                }
            }
        }

    }
}
