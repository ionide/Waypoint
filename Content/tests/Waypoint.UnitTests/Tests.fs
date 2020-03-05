module Tests

open Expecto

let tests =
    testList "Group of tests" [
        test "A simple test" {
            let subject = "Hello World"
            Expect.equal subject "Hello World" "The strings should equal"
        }

        testProperty "Reverse of reverse of a list is the original list" (
            fun (xs:list<int>) -> List.rev (List.rev xs) = xs
        )
    ]
