with import <nixpkgs> {};
  mkShell rec {
    dotnetPkg = dotnetCorePackages.sdk_8_0;

    deps = [
      dotnetPkg
      desktop-file-utils
    ];

    shellHook = ''
      DOTNET_ROOT="${dotnetPkg}";
      export LD_LIBRARY_PATH="$LD_LIBRARY_PATH:${ # glibc polkit
        with pkgs;
          lib.makeLibraryPath [pkg-config fontconfig xorg.libX11 xorg.libICE xorg.libSM]
      }"
    '';
    nativeBuildInputs = [] ++ deps;
  }
