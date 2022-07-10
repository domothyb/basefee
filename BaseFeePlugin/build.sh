#!/bin/bash
echo Building Plugin
dotnet build --configuration Release
echo Building Runner
cd ../Nethermind.Runner
dotnet build
echo Copying DLL
copy ../BaseFeePlugin/bin/Release/net6.0/BaseFeePlugin.dll bin/Release/net6.0/plugins
echo Running Nethermind
dotnet run -c Release --no-build --config ../BaseFeePlugin/mainnet