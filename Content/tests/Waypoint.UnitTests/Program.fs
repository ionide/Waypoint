open System
open Expecto
open Tests

[<EntryPoint>]
let main args =
    runTestsWithArgs defaultConfig args tests
