# Danganronpa: Another Tool
## Version: 0.9 (Release: 16th May 2016)
		
Since this is the first tool I've ever coded so far in C#, it will most likely not be very
user-friendly. If you have to report any problem or bug, feel free to contact me at:
- http://steamcommunity.com/sharedfiles/filedetails/?id=685935319
- all.ice.team@gmail.com		
			 
# Table of Contents
## 1 — Compatibility/Game Titles Compatible with the Tool
## 2 — Usage and Requirements
###  2.1 - "Unpack umdimage.dat" and "Unpack umdimage2.dat"
###  2.2 - "Unpack .wad" and "Re-Pack .wad"
###  2.3 - "Unpack .lin" and "Re-Pack .lin"
###  2.4 - "Unpack .pak" and "Re-Pack .pak"
####    2.4.1 - TextMenu
####	2.4.2 - ScriptText Type1
####	2.4.3 - ScriptText Type2
####	2.4.4 - Font
####	2.4.5 - Textures (TGA)
####	2.4.6 - Textures (PNG)
###  2.5 - "Convert TGA-GXT-BXT to PNG"
###  2.6 - "Convert PNG to TGA" 
## 3 — Changelog
## 4 — Credits

If you're planning on translating DR1 or DR2, please consider there are projects already
in process of working on them, which you can also join. 

## List of DR1 (STEAM) fan-made translations:
- Italian: http://alliceteam.altervista.org/ (by All-Ice Team)
- Spanish: http://transcene.altervista.org/blog/ (by TranScene)
- French: https://equipemirai.wordpress.com/ (by Mirai Team)
- Japanese: http://steamcommunity.com/app/413410/discussions/0/392185054116178261/ (by Fuyuhiko Kuzuryuu)
- Russian: http://www.zoneofgames.ru/forum/index.php?showtopic=37720

## List of DR2 (STEAM) fan-made translations:
- Italian: http://alliceteam.altervista.org/ (by All-Ice Team)
- Spanish: http://transcene.altervista.org/blog/ (by TranScene)
- Japanese: http://steamcommunity.com/app/413420/discussions/0/364041776199905601/ (by Fuyuhiko Kuzuryuu)

# 1 — Compatibility/Game Titles Compatible with the Tool
By using this program, you will be able to extract *all the files from:
- Danganronpa: Trigger Happy Havoc (PSP DEMO - PSP - PSVITA - STEAM)
- Danganronpa 2: Goodbye Despair (PSVITA - STEAM)
- Danganronpa Another Episode: Ultra Despair Girls (PSVITA)

However, since this is mainly planned to work on the Steam version, you will only be able to repack modified files into:
- Danganronpa: Trigger Happy Havoc (STEAM)
- Danganronpa 2: Goodbye Despair (STEAM)

*I can't help you with the extraction of the contents of the ".cpk" files from PSVITA,
what I can help you with, however, is with what you are going to use:
http://aluigi.altervista.org/quickbms.htm
 
# 2 — Usage and Requirements
## Requirements
- .NET Framework 4
- If you plan to work with PSVita textures, these softwares will be required:
Python 2.7 (x86) -> http://www.python.org/download/
PyQt4 (for shtx_conv) -> http://www.riverbankcomputing.co.uk/software/pyqt/download
- If you have a 32-bit operative system, you have to download the 32-bit version of
"convert.exe" from the official website of the program and replace "convert.exe"
which is found in the "Ext" folder.
Official program website: http://www.imagemagick.org/script/binary-releases.php
- The folder "Ext" has to be next to the tool so that it can work properly.

First of all, select the game you want to work on via the drop-down menu located near
"Working on:".

##  2.1 - "Unpack umdimage.dat" and "Unpack umdimage2.dat"
These two features will extract all the content from the demo and from the retail
game of DR1 for PSP. It only works with a modded umdimage.dat/umdimage2.dat archive
rebuilt with the Super Duper Script Editor. In order to extract data, you have to
point out the type of the archive you are going to work on, be it demo or retail,
from the drop-down menu located on the left of "Unpack umdimage.dat".
https://bitbucket.org/blackdragonhunt/the-super-duper-script-editor

##  2.2 - "Unpack .wad" and "Re-Pack .wad" (STEAM Only)
These will allow you to completely unpack and recreate from scratch the WAD archives
of the game, that means that you can even create WADs different from the original.

##  2.3 - "Unpack .lin" and "Re-Pack .lin"
These two will let you extract and repack the text located into the .lin files.
If you don't need information on the speaker, check the "No Names" checkbox.
If you are working on the PSP extracted .lin and you must remove the CLT tags
used in the PSP version, then check the "Clean PSP CLT" checkbox.

##  2.4 - "Unpack .pak" and "Re-Pack .pak"
There are different .pak files, some store textures, while others store texts. They
are a generic archive container, that's why you can choose the type of .pak you are
going to work on from the drop-down menu located on the left of "Unpack .pak".

- 2.4.1 - TextMenu: It works for files like "00_System.pak", "26_Menu.pak",
        "mtb_s03.pak", "49_Novel.pak", "mtb2_s06.pak", etc.

- 2.4.2 - ScriptText Type1: It works for files like "script_pak_e02.pak",
        "script_pak_novel.pak", "script_pak_e07.pak", etc.

- 2.4.3 - ScriptText Type2: It works for files like "bin_special_font_l.pak",
        "bin_pb_font_l.pak", "bin_sv_font_l.pak", etc.

- 2.4.4 - Font: It only works with the "font.pak". If you have to create your
		own font, I recommend you to use the Super Duper Script Editor.

- 2.4.5 - Textures (TGA): It works for all the ".pak" files, that use to store
		textures for the most part of them. Textures will be extracted as TGAs.
		This tool will automatically encode them back into the right format
		while repacking everything back.

- 2.4.6 - Textures (PNG): It works for all the ".pak" files, that use to store
		textures for the most part of them. Textures will be extracted as PNGs.		
		This tool will automatically encode them back into the right format
		while repacking everything back.

##  2.5 - "Convert TGA/GXT/BXT to PNG"
Just like the name suggests, it converts textures from TGA/GXT/BXT -> PNG.

##  2.6 - "Convert PNG to TGA"
Just like the name suggests, it converts textures from PNG -> TGA.
 
# 3 — Changelog
## Version: 0.9 (Release: 16th May 2016)
- First public release.

# 4 — Credits 
I'd like to thank Sorakairi, from Deep Dive Translations, for being my teacher on this,
and "Project Zetsubou" team for the font-editor they created for the PSP version,
which also works for Steam's, and for making public the OP Codes for DR1 & DR2.

Project Zetsubou: https://danganronpa.wordpress.com/
Deep Dive Translations: http://deepdivetranslations.altervista.org/

Other programs that are used by the tool, which can be found in "Ext" folder:

- "convert.exe" is actually used to convert textures from ".TGA" to ".PNG" and viceversa
  Thanks to: http://www.imagemagick.org/script/convert.php

- "dr_dec.py", "shtx_conv.py", "util.py" and "util.pyc" are actually used to decrypt
  textures from the PSVITA version of the games.
  Thanks to: https://github.com/BlackDragonHunt/Danganronpa-Tools

- "GXTConvert.exe" is actually used to convert textures from ".GXT" to ".PNG"
  Thanks to: https://github.com/xdanieldzd/GXTConvert
