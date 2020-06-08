### 0.3.0 - 08.06.2020
* Exclude empty changes from the changelog list [#19](https://github.com/ionide/Waypoint/pull/19) by [@NicoVIII](https://github.com/NicoVIII)
* Fix documentation build for Unix systems [#17](https://github.com/ionide/Waypoint/pull/17) by [@NicoVIII](https://github.com/NicoVIII)
* Use defined properties in Pack target [#18](https://github.com/ionide/Waypoint/pull/18) by [@NicoVIII](https://github.com/NicoVIII)
* Fix index redirect for root domains with subfolder [#20](https://github.com/ionide/Waypoint/pull/20) by [@NicoVIII](https://github.com/NicoVIII)
* Support F#.Formatting literal programming by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)
* Remove Markdig by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)
* Update to F#.Formatting 4.1 by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)

### 0.2.0 - 18.05.2020
* Fix div typo [#12](https://github.com/ionide/Waypoint/pull/12) by [@ChrisNikkel](https://github.com/ChrisNikkel)
* Add ToolType to Paket.push in build.fsx [#14](https://github.com/ionide/Waypoint/pull/14) by [@NicoVIII](https://github.com/NicoVIII)
* Add rollForward policy to global.json [#13](https://github.com/ionide/Waypoint/pull/13) by [@NicoVIII](https://github.com/NicoVIII)
* Port latest updates from Saturn documentation by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)
* Update Fornax version to `0.13.1` by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)
* Update `build.fsx` script by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)
* Add `publish` GitHub action by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)

### 0.1.0 - 07.04.2020
* Update build instructions [#6](https://github.com/ionide/Waypoint/pull/6) by [@ZaymonFC](https://github.com/ZaymonFC)
* Added `.editorconfig` [#5](https://github.com/ionide/Waypoint/pull/5) by [@jbeeko](https://github.com/jbeeko)
* Switch to Fake CreateProcessAPI [#7](https://github.com/ionide/Waypoint/pull/7) by [@jbeeko](https://github.com/jbeeko)
* Fix link to ApiReference [#9](https://github.com/ionide/Waypoint/pull/9) by [@NicoVIII](https://github.com/NicoVIII)
* Replace usage of "Saturn" in CONTRIBUTING.md [#8](https://github.com/ionide/Waypoint/pull/8) by [@NicoVIII](https://github.com/NicoVIII)
* Use nugetDir for Paket.push [#10](https://github.com/ionide/Waypoint/pull/10) by [@NicoVIII](https://github.com/NicoVIII)
* Replace "FancyApp" in documentation by "Waypoint" [#11](https://github.com/ionide/Waypoint/pull/11) by [@NicoVIII](https://github.com/NicoVIII)
* Update Fornax version to `0.11.1`  by [@Krzysztof-Cieslak](https://github.com/Krzysztof-Cieslak)

### 0.0.2 - 05.03.2020

* Initial release
* What's included:
    - Paket, FAKE, and Fornax added as `dotnet` local tools (`.config/dotnet-tools.json`)
    - `build.fsx` file, containing default FAKE script with targets for building, testing, documentation generation, publishing to GitHub, and publishing to NuGet
    - `paket.dependencies` with basic set of dependencies
    - `src` folder containing 2 projects - one class library (`netstandard2.0`), and CLI tool (`netcoreapp3.1`)
    - `test` folder containing UnitTest project using Expecto and FsCheck
    - `docs` folder with Fornax documentation template that will generate nice documentation for your project.
    - `.devcontainer` folder with definition of [Development Container](https://code.visualstudio.com/docs/remote/containers)
    - `.github/workflows` folder with definition for 2 GitHub actions - one for building and testing code as CI, one for deploying documentation when new tag is pushed. To use latter, you need to define `PERSONAL_TOKEN` secret in GitHub repo settings with Personal Access Token.
    - `.github/ISSUE_TEMPLATE` folder with 2 different issue templates - one for bug report, other one for feature request