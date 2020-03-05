### 0.0.1 - 05.03.2020

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