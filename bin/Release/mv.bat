set GAMEDIR=C:\Program Files (x86)\Steam\steamapps\common\SUPERHOT
set GAMENAME=SH

::copy /Y "ViewRMod\bin\x64\Release\ViewRMod.dll" "%GAMEDIR%\Plugins\"
copy /Y "PressPToPause.dll" "%GAMEDIR%\Plugins\"
mkdir "%GAMEDIR%\%GAMENAME%_Data\StreamingAssets"
::copy /Y "..\..\unity-plugin-scene\AssetBundles\StandaloneWindows64\viewr.asset" "%GAMEDIR%\%GAMENAME%_Data\StreamingAssets\viewr.asset"
::mkdir "%GAMEDIR%\%GAMENAME%_Data\Plugins"
::copy /Y "..\..\native-unity-plugin\build\x64\Release\gfxplugin-viewr.dll" "%GAMEDIR%\%GAMENAME%_Data\Plugins\gfxplugin-viewr.dll"

cd "%GAMEDIR%"
call "%GAMEDIR%\%GAMENAME%.exe" --verbose