@REM Clear destination folder
erase /F /S /Q FlowchartBuilder\*.*

@REM Create subdirs
mkdir FlowchartBuilder\Translations
mkdir FlowchartBuilder\Plugins
mkdir FlowchartBuilder\Icons
mkdir FlowchartBuilder\Graphics
mkdir FlowchartBuilder\Data
mkdir FlowchartBuilder\Docs
mkdir FlowchartBuilder\Examples

@REM Copy program
copy ..\Makarov.FlowchartBuilder\Makarov.FlowchartBuilder.exe FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.FlowchartBuilder.Box.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.FlowchartBuilder.API.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Collections.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Components.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Core.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Graphics.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Instance.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Math.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Makarov.Framework.Serialization.dll FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\License.txt FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Settings.xml FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\Settings.xsd FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\BackwardCompatibility.xml FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\BackwardCompatibility.xsd FlowchartBuilder

@REM Copy program icon
copy ..\Makarov.FlowchartBuilder\FlowchartFile.ico FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\FlowchartBuilder.ico FlowchartBuilder
copy ..\Makarov.FlowchartBuilder\FlowchartSettings.ico FlowchartBuilder

@REM Copy translations
copy ..\Makarov.FlowchartBuilder\Translations\*.xml FlowchartBuilder\Translations
copy ..\Makarov.FlowchartBuilder\Translations\*.xsd FlowchartBuilder\Translations

@REM Copy plugins
copy ..\Makarov.FlowchartBuilder\Plugins\*.dll FlowchartBuilder\Plugins

@REM Copy icons
copy ..\Makarov.FlowchartBuilder\Icons\*.ico FlowchartBuilder\Icons
copy ..\Makarov.FlowchartBuilder\Icons\*.bmp FlowchartBuilder\Icons
copy ..\Makarov.FlowchartBuilder\Icons\*.jpg FlowchartBuilder\Icons
copy ..\Makarov.FlowchartBuilder\Icons\*.png FlowchartBuilder\Icons

@REM Copy graphics
copy ..\Makarov.FlowchartBuilder\Graphics\*.ico FlowchartBuilder\Graphics
copy ..\Makarov.FlowchartBuilder\Graphics\*.bmp FlowchartBuilder\Graphics
copy ..\Makarov.FlowchartBuilder\Graphics\*.jpg FlowchartBuilder\Graphics
copy ..\Makarov.FlowchartBuilder\Graphics\*.png FlowchartBuilder\Graphics

@REM Copy data
copy ..\Makarov.FlowchartBuilder\Data\*.xml FlowchartBuilder\Data
copy ..\Makarov.FlowchartBuilder\Data\*.xsd FlowchartBuilder\Data

@REM Copy docs
copy ..\Makarov.FlowchartBuilder\Docs\*.* FlowchartBuilder\Docs

@REM Copy examples
copy ..\Makarov.FlowchartBuilder\Examples\*.* FlowchartBuilder\Examples

@REM Obfuscation
@REM rmdir /S /Q FlowchartBuilder\Obfuscated
@REM mkdir FlowchartBuilder\Obfuscated
@REM "C:\Program Files (x86)\{smartassembly}\{smartassembly}.com" "C:\MakarovProject\Make\FlowchartBuilder.{sa}proj"

@REM rmdir /S /Q FlowchartBuilder\Obfuscated

@REM Create installation
@REM erase FlowchartBuilder.exe
@REM "C:\Program Files (x86)\Inno Setup 5\ISCC.exe" FlowchartBuilder.iss