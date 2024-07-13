{
  buildDotnetModule,
  dotnetCorePackages,
  fetchFromGitHub,
  copyDesktopItems,
  makeDesktopItem,
  pkgs,
  lib,
}:
buildDotnetModule rec {
  pname = "NexusMods.App";

  version = "0.5.2";

  src = fetchFromGitHub {
    owner = "Nexus-Mods";
    repo = "NexusMods.App";
    rev = "e9c6f856dc0828d7c94ee52530be1ea1933c6e41";
    fetchSubmodules = true;
    hash = "sha256-KYwDvZMe7PQjhBr/Sb6QVkzf1npVPMjCriacGIKW4SQ=";
  };

  # projectFile = "src/NexusMods.App/NexusMods.App.csproj";
  projectFile = [
    "src/NexusMods.StandardGameLocators/NexusMods.StandardGameLocators.csproj"
    "tests/NexusMods.StandardGameLocators.Tests/NexusMods.StandardGameLocators.Tests.csproj"
    "src/NexusMods.App.Cli/NexusMods.App.Cli.csproj"
    "src/NexusMods.App/NexusMods.App.csproj"
    "src/NexusMods.DataModel/NexusMods.DataModel.csproj"
    "src/ArchiveManagement/NexusMods.FileExtractor/NexusMods.FileExtractor.csproj"
    "src/NexusMods.App.BuildInfo/NexusMods.App.BuildInfo.csproj"
    "src/NexusMods.App.UI/NexusMods.App.UI.csproj"
    "src/Networking/NexusMods.Networking.NexusWebApi/NexusMods.Networking.NexusWebApi.csproj"
    "src/Networking/NexusMods.Networking.HttpDownloader/NexusMods.Networking.HttpDownloader.csproj"
    "src/Games/NexusMods.Games.RedEngine/NexusMods.Games.RedEngine.csproj"
    "src/Games/NexusMods.Games.TestHarness/NexusMods.Games.TestHarness.csproj"
    "src/Games/NexusMods.Games.Reshade/NexusMods.Games.Reshade.csproj"
    "src/Games/NexusMods.Games.Generic/NexusMods.Games.Generic.csproj"
    "benchmarks/NexusMods.Benchmarks/NexusMods.Benchmarks.csproj"
    "src/Games/NexusMods.Games.FOMOD/NexusMods.Games.FOMOD.csproj"
    "src/Games/NexusMods.Games.StardewValley/NexusMods.Games.StardewValley.csproj"
    "src/Networking/NexusMods.Networking.Downloaders/NexusMods.Networking.Downloaders.csproj"
    "src/Games/NexusMods.Games.FOMOD.UI/NexusMods.Games.FOMOD.UI.csproj"
    "src/Games/NexusMods.Games.AdvancedInstaller/NexusMods.Games.AdvancedInstaller.csproj"
    "src/Games/NexusMods.Games.AdvancedInstaller.UI/NexusMods.Games.AdvancedInstaller.UI.csproj"
    "src/Abstractions/NexusMods.Abstractions.Installers/NexusMods.Abstractions.Installers.csproj"
    "src/Abstractions/NexusMods.Abstractions.IO/NexusMods.Abstractions.IO.csproj"
    "src/Extensions/NexusMods.Extensions.BCL/NexusMods.Extensions.BCL.csproj"
    "src/Abstractions/NexusMods.Abstractions.Serialization/NexusMods.Abstractions.Serialization.csproj"
    "src/Abstractions/NexusMods.Abstractions.Games/NexusMods.Abstractions.Games.csproj"
    "src/Abstractions/NexusMods.Abstractions.Games.Diagnostics/NexusMods.Abstractions.Games.Diagnostics.csproj"
    "src/Extensions/NexusMods.Extensions.DependencyInjection/NexusMods.Extensions.DependencyInjection.csproj"
    "src/NexusMods.CrossPlatform/NexusMods.CrossPlatform.csproj"
    "src/Extensions/NexusMods.Extensions.Hashing/NexusMods.Extensions.Hashing.csproj"
    "src/Abstractions/NexusMods.Abstractions.Activities/NexusMods.Abstractions.Activities.csproj"
    "src/Abstractions/NexusMods.Abstractions.FileExtractor/NexusMods.Abstractions.FileExtractor.csproj"
    "src/Abstractions/NexusMods.Abstractions.GuidedInstallers/NexusMods.Abstractions.GuidedInstallers.csproj"
    "src/Abstractions/NexusMods.Abstractions.Cli/NexusMods.Abstractions.Cli.csproj"
    "src/Abstractions/NexusMods.Abstractions.NexusWebApi/NexusMods.Abstractions.NexusWebApi.csproj"
    "src/NexusMods.Activities/NexusMods.Activities.csproj"
    "src/Themes/NexusMods.Themes.NexusFluentDark/NexusMods.Themes.NexusFluentDark.csproj"
    "src/Extensions/NexusMods.Extensions.DynamicData/NexusMods.Extensions.DynamicData.csproj"
    "src/Abstractions/NexusMods.Abstractions.HttpDownloader/NexusMods.Abstractions.HttpDownloader.csproj"
    "src/Abstractions/NexusMods.Abstractions.Loadouts/NexusMods.Abstractions.Loadouts.csproj"
    "src/Abstractions/NexusMods.Abstractions.FileStore/NexusMods.Abstractions.FileStore.csproj"
    "src/Abstractions/NexusMods.Abstractions.DiskState/NexusMods.Abstractions.DiskState.csproj"
    "src/Abstractions/NexusMods.Abstractions.Loadouts.Synchronizers/NexusMods.Abstractions.Loadouts.Synchronizers.csproj"
    "src/Abstractions/NexusMods.Abstractions.GameLocators/NexusMods.Abstractions.GameLocators.csproj"
    "src/NexusMods.App.Generators.Diagnostics/NexusMods.App.Generators.Diagnostics/NexusMods.App.Generators.Diagnostics.csproj"
    "src/NexusMods.App.Generators.Diagnostics/NexusMods.App.Generators.Diagnostics.Sample/NexusMods.App.Generators.Diagnostics.Sample.csproj"
    "src/Games/NexusMods.Games.StardewValley.SMAPI/NexusMods.Games.StardewValley.SMAPI.csproj"
    "src/Examples/Examples.csproj"
    "src/NexusMods.Icons/NexusMods.Icons.csproj"
    "src/Abstractions/NexusMods.Abstractions.Settings/NexusMods.Abstractions.Settings.csproj"
    "src/NexusMods.Settings/NexusMods.Settings.csproj"
    "src/Abstractions/NexusMods.Abstractions.MnemonicDB.Attributes/NexusMods.Abstractions.MnemonicDB.Attributes.csproj"
    "src/Abstractions/NexusMods.Abstractions.Telemetry/NexusMods.Abstractions.Telemetry.csproj"
    "src/NexusMods.Telemetry/NexusMods.Telemetry.csproj"
    "src/NexusMods.ProxyConsole.Abstractions/NexusMods.ProxyConsole.Abstractions.csproj"
    "src/NexusMods.ProxyConsole/NexusMods.ProxyConsole.csproj"
    "src/NexusMods.SingleProcess/NexusMods.SingleProcess.csproj"
    "src/Abstractions/NexusMods.Abstractions.Library/NexusMods.Abstractions.Library.csproj"
    "src/Abstractions/NexusMods.Abstractions.NexusModsLibrary/NexusMods.Abstractions.NexusModsLibrary.csproj"
    "src/Abstractions/NexusMods.Abstractions.Downloads/NexusMods.Abstractions.Downloads.csproj"
    "src/NexusMods.Library/NexusMods.Library.csproj"
  ];

  nugetDeps = ./deps.nix;
  dontStrip = true;

  dotnet-sdk = dotnetCorePackages.sdk_8_0;
  dotnet-runtime = dotnetCorePackages.sdk_8_0;

  # preConfigure = ''
  #   git submodule update --init --recursive
  # '';

  nativeBuildInputs = with pkgs; [
    copyDesktopItems
    pkg-config
  ];

  runtimeDeps = with pkgs; [
    fontconfig
    xorg.libX11
    xorg.libICE
    xorg.libSM
  ];

  desktopItems = [
    (makeDesktopItem {
      name = "NexusMods.App";
      exec = "${dotnet-runtime}/bin/dotnet NexusMods.App.dll";
      icon = "zdl3";
      desktopName = "NexusMods.App";
    })
  ];

  makeWrapperArgs = [
    "--prefix PATH : ${lib.makeBinPath [pkgs.desktop-file-utils]}"
  ];
}
