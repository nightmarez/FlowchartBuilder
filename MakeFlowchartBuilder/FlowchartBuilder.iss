[Dirs]
Name: {app}\Icons
Name: {app}\Translations
Name: {app}\Data
Name: {app}\Graphics
Name: {app}\Plugins
Name: {app}\Docs
Name: {app}\Examples
[Icons]
Name: {group}\FlowchartBuilder; Filename: {app}\Makarov.FlowchartBuilder.exe; WorkingDir: {app}; IconFilename: {app}\Icon.ico; IconIndex: 0
Name: {group}\Uninstall; Filename: {app}\unins000.exe; WorkingDir: {app}
Name: {userdesktop}\FlowchartBuilder; Filename: {app}\Makarov.FlowchartBuilder.exe; WorkingDir: {app}; IconFilename: {app}\Icon.ico; IconIndex: 0
[Setup]
InternalCompressLevel=ultra64
OutputBaseFilename=FlowchartBuilderInstall
SolidCompression=true
Compression=lzma/ultra64
AppCopyright=Michael Makarov
AppName=FlowchartBuilder
AppVerName=FlowchartBuilder (alpha)
DirExistsWarning=yes
DefaultDirName={pf}\FlowchartBuilder
DefaultGroupName=FlowchartBuilder
OutputDir=C:\MakarovProject\Make
UninstallLogMode=append
LicenseFile=C:\MakarovProject\Make\FlowchartBuilder\License.txt
VersionInfoCopyright=Michael Makarov
EnableDirDoesntExistWarning=true
WindowVisible=true
WindowShowCaption=false
WindowResizable=false
[Files]
Source: FlowchartBuilder\Makarov.FlowchartBuilder.exe; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Makarov.Framework.Core.dll; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Common.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Links.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Shapes.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Simple.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Uml.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Plugins.BasicPlugin.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\MainMenu.xml; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\MainMenu.xsd; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\SecondaryMenu.xml; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\SecondaryMenu.xsd; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\Units.xml; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Data\Units.xsd; DestDir: {app}\Data; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Translations\English.xml; DestDir: {app}\Translations; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Translations\Translations.xsd; DestDir: {app}\Translations; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\License.txt; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icon.ico; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Translations\Russian.xml; DestDir: {app}\Translations; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Translations\Ukrainian.xml; DestDir: {app}\Translations; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Graphics\Image.png; DestDir: {app}\Graphics; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Graphics\Pointer.png; DestDir: {app}\Graphics; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\ActualSize.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignBottom.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignBottomSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignCenterHorizontal.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignCenterHorizontalSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignCenterVertical.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignCenterVerticalSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignLeft.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignLeftSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignRight.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignRightSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignTop.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\AlignTopSheet.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Copy.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Cut.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Delete.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Deselect.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\English.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Grid.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Group.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\HelpTopics.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Open.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\PageSetup.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Paste.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Print.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Redo.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Rulers.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Russian.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Save.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\SaveAll.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\SelectAll.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\SizeLocationLabel.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Ukrainian.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Undo.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Ungroup.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\Zoom.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\ZoomIn.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\ZoomOut.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Icons\ZoomToWindow.bmp; DestDir: {app}\Icons; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Settings.xml; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Settings.xsd; DestDir: {app}; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
Source: FlowchartBuilder\Plugins\Makarov.FlowchartBuilder.Glyphs.Graphs.dll; DestDir: {app}\Plugins; Flags: overwritereadonly ignoreversion uninsremovereadonly replacesameversion setntfscompression
