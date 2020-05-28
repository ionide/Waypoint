namespace Waypoint

///Some comment on the module
module Say =

    ///Some comment on the function
    ///
    /// ```
    /// let sample = hello "Chris"
    /// sample = "Hello Chris"
    /// ```
    let hello name =
        printfn "Hello %s" name

    ///Some comment on the type
    type SampleType = {
        ///Some more comments
        A: string
        ///And more comments
        B: int

    }
