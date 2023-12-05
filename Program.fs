
module Program
open Manager
open System


/// <summary>
/// Author: Josh Mentele
/// Description: This function handles the print matrix menu option, prompting for the index of
/// the matrix to print, and outputting the result.
/// </summary>
/// <param name="manager">The matrix manager.</param>
let printMatrix(manager: MatrixManager) : unit = 
    // prompt for index
    printf "Enter the matrix index:> "

    try
        // get input
        let index = System.Int32.Parse(Console.ReadLine().Trim())
        manager.PrintMatrix(index)
    with
    | :? FormatException -> printfn "Please input valid numbers\n"
    | :? InvalidOperationException as ex -> printfn "%s\n" ex.Message


/// <summary>
/// Author: Josh Mentele
/// Description: This function handles the create matrix menu option, prompting for the number
/// of rows and cols and the matrix's values, and stores the matrix in the manager.
/// </summary>
/// <param name="manager"></param>
let createMatrix(manager: MatrixManager) : unit =

    // prompt rows
    printf "Enter the number of rows:> "

    try
        let rows = System.Int32.Parse(Console.ReadLine().Trim())
        
        // prompt for cols
        printf "Enter the number of cols:> "

        let cols = System.Int32.Parse(Console.ReadLine().Trim())

        // prompt for values
        printf "Enter the values for the matrix (there should be %d values):> " (rows * cols)

        let mutable values = [||]

        // get values from user
        while values.Length <> rows * cols do
            let input = Console.ReadLine().Trim()

            // split up input
            let inputSplit = input.Split " "

            // convert to integer values
            for i in inputSplit do
                values <- Array.append values [|System.Double.Parse(i)|]
        
        manager.CreateMatrix(rows, cols, values)
    with
    | :? FormatException -> printfn "Please input valid numbers\n"
    | :? InvalidOperationException as ex -> printfn "%s\n" ex.Message


/// <summary>
/// Author: Josh Mentele
/// Description: This function handles the menu option for adding matrices, prompting for the matrix indices
/// and printing out the result.
/// </summary>
/// <param name="manager">The matrix manager.</param>
let addMatrices(manager: MatrixManager) : unit =
    printf "Enter the first matrix index:> "

    try
        // get input
        let idx1 = System.Int32.Parse(Console.ReadLine().Trim())

        printf "Enter the second matrix index:> "

        // get input
        let idx2 = System.Int32.Parse(Console.ReadLine().Trim())

        printfn "%s" (manager.AddMatrices(idx1, idx2).ToString())
    with
    | :? FormatException -> printfn "Please input a valid integer\n"
    | :? InvalidOperationException as ex -> printfn "%s\n" ex.Message


/// <summary>
/// Author: Tobias Lynch
/// Description: This function handles the menu option for multiplying a matrix by a scalar, prompting for
/// the matrix index and scalar and printing out the result.
/// </summary>
/// <param name="manager">The matrix manager.</param>
let multMatrixByScalar(manager: MatrixManager) : unit =
    printf "Enter the matrix index:> "

    try
        //Get input
        let id : int = System.Int32.Parse(Console.ReadLine().Trim())

        printf "Enter the scalar value:> "

        //Get input
        let scalar: double = System.Double.Parse(Console.ReadLine().Trim())

        printfn "%s" (manager.ScalarMult(id, scalar).ToString())
    with
    | :? FormatException -> printfn "Please input a valid input\n"
    | :? InvalidOperationException as ex -> printfn "%s\n" ex.Message

/// <summary>
/// Author: Tobias Lynch
/// Description: This function handles the menu option for multiplying matrices, prompting for the matrix indices
/// and printing out the result.
/// </summary>
/// <param name="manager">The matrix manager.</param>
let multMatricies(manager: MatrixManager) : unit =
    printf "Enter the first matrix id:> "

    try
        //Get input
        let idx1: int = System.Int32.Parse(Console.ReadLine().Trim())
        
        printf "Enter the second matrix id:> "

        //Get input
        let idx2: int = System.Int32.Parse(Console.ReadLine().Trim())

        printfn "%s" (manager.MatrixMult(idx1, idx2).ToString())
    with
    | :? FormatException -> printfn "Please input a valid integer\n"
    | :? InvalidOperationException as ex -> printfn "%s\n" ex.Message


[<EntryPoint>]
let main args =
    let manager = new MatrixManager()

    let menu: string = ("1.) Create Matrix\n" + 
                        "2.) Print Matrix\n" + 
                        "3.) List Matrices\n" +
                        "4.) Add Matrices\n" +
                        "5.) Subtract Matrices\n" +
                        "6.) Multiply Matrices\n" +
                        "7.) Multiply Matrix by Scalar\n" + // NOTE: Do this option in parallel
                        "0.) Quit\n" +
                        "Enter Selection:> ")

    // holds selection
    let mutable selection = -1

    while selection <> 0 do
        // print menu
        printf "%s" menu


        // get selection
        let input = Console.ReadLine().Trim()

        // parse input
        let (success, i) = System.Int32.TryParse input
        selection <- i

        if not success then
            printfn "Please select a valid menu option.\n"
        else
            match selection with
            | 0 -> printfn "Exiting..."
            | 1 -> createMatrix(manager)
            | 2 -> printMatrix(manager)
            | 3 -> manager.PrintMatrices()
            | 4 -> addMatrices(manager)
            | 6 -> multMatricies(manager)
            | 7 -> multMatrixByScalar(manager)
            | _ -> printfn "Please select a valid menu option.\n"

    0
    