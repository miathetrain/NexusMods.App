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

  projectFile = "src/NexusMods.App/NexusMods.App.csproj";

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
