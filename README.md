# DotNetDebloat
This repository contains dotNet tools to address dotNet binary bloat.

This is a supplimentary repository to https://github.com/Squiblydoo/debloat. Debloat is able to handle a majority inflated binaries automatically. A faction of the remainder consist of .NET binaries. This repository is to supplement that limitation.

Unlike debloat, these tools are unable to function automatically and require some literacy and awareness of the technique being used: a future goal is that the functionality of these tools will be incorporated into future debloat releases.
Unlike debloat, both tools are only built for Windows and are only CommandLine applications.

# Tools
## Summary 
This repository consists of two tools which can be downloaded from the Releases.

## SingleFileSolution
This .NET EXE can be used to extract the contents of a SingleFile .NET Executable.
SingleFileSolution takes one argument, the name of the SingleFile executable to be unpacked. The output will be a directory with all of the contents unpacked. Analysis and identifying suspicious content from the output is the responsibility of the user.

### Identifying SingleFile executables
This guidance may be updated at a later time.
But SingleFile executables can be identified by having a large PE Overlay. The PE overlay will contain a large number of PE files. Contents of the overlay can be identified with a binary analysis tool such as Malcat. The PE carved from the overlay and the large overlay are highlighted in the image below.
![image](https://github.com/Squiblydoo/DotNetDebloat/assets/77356206/f0979984-cd27-45fe-af80-72013724acf2)


## LargeResResolver
This .NET EXE can be used to remove excessively large .NET resources from an executable.
LargeResResolver takes one argument, the name of the EXE with content to be removed. The output will be an executable that has the junk removed. The file can then be analyzed without difficulty caused by the inflated content.

### Identifying files with large .NET Resource
Files with large .NET resources can be identified by processing the file with Debloat. Debloat will detect that the .TEXT section of an executable is inflated and output the message `Bloat was detected in the text section. Bloat is likely in a .NET Resource`. 
Alternatively, an inflated .TEXT resource can be identified by reviewing the file in an analysis tool. See inflated .TEXT section highlighted in the image below. The image is of the analysis tool Malcat.
![image](https://github.com/Squiblydoo/DotNetDebloat/assets/77356206/51bcbaae-0497-4fc3-b42a-484fca4f9a27)
