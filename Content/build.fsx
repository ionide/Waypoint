// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket: groupref build //"
#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.Tools
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.Api

// --------------------------------------------------------------------------------------
// Information about the project to be used at NuGet and in AssemblyInfo files
// --------------------------------------------------------------------------------------

let summary = "{SUMMARY}"
let authors = "{AUTHORS}"
let tags = "{PACKAGETAGS}"
let copyright = "{COPYRIGHT}"

let gitOwner = "{GITOWNER}"
let gitName = "{PROJECTNAME}"
let gitHome = "https://github.com/" + gitOwner
let gitUrl = gitHome + "/" + gitName

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir  = "./build/"
let nugetDir  = "./out/"


System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
let changelogFilename = "CHANGELOG.md"
let changelog = Changelog.load changelogFilename
let latestEntry = changelog.LatestEntry

// Helper function to remove blank lines
let isEmptyChange = function
    | Changelog.Change.Added s
    | Changelog.Change.Changed s
    | Changelog.Change.Deprecated s
    | Changelog.Change.Fixed s
    | Changelog.Change.Removed s
    | Changelog.Change.Security s
    | Changelog.Change.Custom (_, s) ->
        String.isNullOrWhiteSpace s.CleanedText

let nugetVersion = latestEntry.NuGetVersion
let packageReleaseNotes = sprintf "%s/blob/v%s/CHANGELOG.md" gitUrl latestEntry.NuGetVersion
let releaseNotes =
    latestEntry.Changes
    |> List.filter (isEmptyChange >> not)
    |> List.map (fun c -> " * " + c.ToString())
    |> String.concat "\n"

// --------------------------------------------------------------------------------------
// Helpers
// --------------------------------------------------------------------------------------
let isNullOrWhiteSpace = System.String.IsNullOrWhiteSpace

let exec cmd args dir =
    let proc =
        CreateProcess.fromRawCommandLine cmd args
        |> CreateProcess.ensureExitCodeWithMessage (sprintf "Error while running '%s' with args: %s" cmd args)
    (if isNullOrWhiteSpace dir then proc
    else proc |> CreateProcess.withWorkingDirectory dir)
    |> Proc.run
    |> ignore

let getBuildParam = Environment.environVar
let DoNothing = ignore
// --------------------------------------------------------------------------------------
// Build Targets
// --------------------------------------------------------------------------------------

Target.create "Clean" (fun _ ->
    Shell.cleanDirs [buildDir; nugetDir]
)

Target.create "Build" (fun _ ->
    DotNet.build id ""
)

Target.create "Test" (fun _ ->
    exec "dotnet" @"run --project .\tests\{PROJECTNAME}.UnitTests\{PROJECTNAME}.UnitTests.fsproj" "."
)

Target.create "Docs" (fun _ ->
    exec "dotnet"  @"fornax build" "docs"
)

// --------------------------------------------------------------------------------------
// Release Targets
// --------------------------------------------------------------------------------------
Target.create "BuildRelease" (fun _ ->
    DotNet.build (fun p ->
        { p with
            Configuration = DotNet.BuildConfiguration.Release
            OutputPath = Some buildDir
            MSBuildParams = { p.MSBuildParams with Properties = [("Version", nugetVersion); ("PackageReleaseNotes", packageReleaseNotes)]}
        }
    ) "{PROJECTNAME}.sln"
)


Target.create "Pack" (fun _ ->
    let properties = [
        ("Version", nugetVersion);
        ("Authors", authors)
        ("PackageProjectUrl", gitUrl)
        ("PackageTags", tags)
        ("RepositoryType", "git")
        ("RepositoryUrl", gitUrl)
        ("PackageLicenseUrl", gitUrl + "/LICENSE")
        ("Copyright", copyright)
        ("PackageReleaseNotes", packageReleaseNotes)
        ("PackageDescription", summary)
        ("EnableSourceLink", "true")
    ]


    DotNet.pack (fun p ->
        { p with
            Configuration = DotNet.BuildConfiguration.Release
            OutputPath = Some nugetDir
            MSBuildParams = { p.MSBuildParams with Properties = properties}
        }
    ) "{PROJECTNAME}.sln"
)

Target.create "ReleaseGitHub" (fun _ ->
    let remote =
        Git.CommandHelper.getGitResult "" "remote -v"
        |> Seq.filter (fun (s: string) -> s.EndsWith("(push)"))
        |> Seq.tryFind (fun (s: string) -> s.Contains(gitOwner + "/" + gitName))
        |> function None -> gitHome + "/" + gitName | Some (s: string) -> s.Split().[0]

    Git.Staging.stageAll ""
    Git.Commit.exec "" (sprintf "Bump version to %s" nugetVersion)
    Git.Branches.pushBranch "" remote (Git.Information.getBranchName "")


    Git.Branches.tag "" nugetVersion
    Git.Branches.pushTag "" remote nugetVersion

    let client =
        let user =
            match getBuildParam "github-user" with
            | s when not (isNullOrWhiteSpace s) -> s
            | _ -> UserInput.getUserInput "Username: "
        let pw =
            match getBuildParam "github-pw" with
            | s when not (isNullOrWhiteSpace s) -> s
            | _ -> UserInput.getUserPassword "Password: "

        // Git.createClient user pw
        GitHub.createClient user pw
    let files = !! (nugetDir </> "*.nupkg")



    // release on github
    let cl =
        client
        |> GitHub.draftNewRelease gitOwner gitName nugetVersion (latestEntry.SemVer.PreRelease <> None) [releaseNotes]
    (cl,files)
    ||> Seq.fold (fun acc e -> acc |> GitHub.uploadFile e)
    |> GitHub.publishDraft//releaseDraft
    |> Async.RunSynchronously
)

Target.create "Push" (fun _ ->
    let key =
        match getBuildParam "nuget-key" with
        | s when not (isNullOrWhiteSpace s) -> s
        | _ -> UserInput.getUserPassword "NuGet Key: "
    Paket.push (fun p -> { p with WorkingDir = nugetDir; ApiKey = key; ToolType = ToolType.CreateLocalTool() }))

// --------------------------------------------------------------------------------------
// Build order
// --------------------------------------------------------------------------------------
Target.create "Default" DoNothing
Target.create "Release" DoNothing

"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Default"

"Clean"
 ==> "BuildRelease"
 ==> "Docs"

"Default"
  ==> "Pack"
  ==> "ReleaseGitHub"
  ==> "Push"
  ==> "Release"

Target.runOrDefault "Default"
