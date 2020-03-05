# Waypooint

## How to get started

1. Add links to GH project in all places marked `TODO: ADD_LINK`
2. Add name of the author in all places marked `TODO: ADD_AUTHOR`
3. Fill content of `src/Directory.Build.props`


## What's included

* Paket, FAKE, and Fornax added as `dotnet` local tools (`.config/dotnet-tools.json`)
* `build.fsx` file, containing default FAKE script with targets for building, testing, documentation generation, publishing to GitHub, and publishing to NuGet
* `paket.dependencies` with basic set of dependencies
* `src` folder containing 2 projects - one class library (`netstandard2.0`), and CLI tool (`netcoreapp3.1`)
* `test` folder containing UnitTest project using Expecto and FsCheck
* `docs` folder with Fornax documentation template that will generate nice documentation for your project.
* `.devcontainer` folder with definition of [Development Container](https://code.visualstudio.com/docs/remote/containers)
* `.github/workflows` folder with definition for 2 GitHub actions - one for building and testing code as CI, one for deploying documentation when new tag is pushed. To use latter, you need to define `PERSONAL_TOKEN` secret in GitHub repo settings with Personal Access Token.
* `.github/ISSUE_TEMPLATE` folder with 2 different issue templates - one for bug report, other one for feature request

## How to build

1. Make sure you've installed .Net Core version defined in [global.json](global.json)
2. Run `dotnet tool install` to install all developer tools required to build the project
3. Run `dotnet fake build` to build default target of [build script](build.fsx)
4. To run tests use `dotnet fake build -t Test`
5. To build documentation use `dotnet fake build -t Docs`

## How to release.

Create release.cmd or release.sh file (already git-ignored) with following content (sample from `cmd`, but `sh` file should be similar):

```
@echo off
cls

SET nuget-key=YOUR_NUGET_KEY
SET github-user=YOUR_GH_USERNAME
SET github-pw=YOUR_GH_PASSWORD_OR_ACCESS_TOKEN

dotnet fake build --target Release
```

## How to contribute

*Imposter syndrome disclaimer*: I want your help. No really, I do.

There might be a little voice inside that tells you you're not ready; that you need to do one more tutorial, or learn another framework, or write a few more blog posts before you can help me with this project.

I assure you, that's not the case.

This project has some clear Contribution Guidelines and expectations that you can [read here](CONTRIBUTING.md).

The contribution guidelines outline the process that you'll need to follow to get a patch merged. By making expectations and process explicit, I hope it will make it easier for you to contribute.

And you don't just have to write code. You can help out by writing documentation, tests, or even by giving feedback about this work. (And yes, that includes giving feedback about the contribution guidelines.)

Thank you for contributing!


## Contributing and copyright

The project is hosted on [GitHub](TODO: ADD_LINK) where you can [report issues](TODO: ADD_LINK), fork
the project and submit pull requests.

The library is available under [MIT license](LICENSE.md), which allows modification and redistribution for both commercial and non-commercial purposes.

Please note that this project is released with a [Contributor Code of Conduct](CODE_OF_CONDUCT.md). By participating in this project you agree to abide by its terms.