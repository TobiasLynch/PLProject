namespace Matrices
    
type Matrix(rows : int, cols : int) =
    // holds matrix values
    let mutable values = Array2D.create<double> rows cols 0.0
    member private this.rows = rows
    member private this.cols = cols


    /// <summary>
    /// Author: Josh Mentele
    /// Description: Sets the values of the matrix (here we use a 1D array to fill)
    /// </summary>
    /// <param name="valuesIn">The values to store in the matrix</param>
    member this.SetValues(valuesIn : double array) =
        if valuesIn.Length <> this.rows * this.cols then
            raise (System.InvalidOperationException "An incorrect number of values were supplied for the matrix")
        else
            for i = 0 to rows - 1 do
                for j = 0 to cols - 1 do
                    values.[i, j] <- valuesIn.[i * cols + j]

    /// <summary>
    /// Author: Josh Mentele
    /// Description: Prints the matrix to a string.</summary>
    /// <param name="unit"></param>
    /// <returns>The matrix string representation</returns>
    override this.ToString() : string =
        let mutable result = ""
        // loop rows
        for i = 0 to rows - 1 do
            result <- $"{result}| "
            // loop columns
            for j = 0 to cols - 1 do
                result <- $"{result}{values.[i, j].ToString().PadLeft(8)} "
            result <- $"{result}|\n"
        result

    /// <summary>
    /// Author: Tobias Lynch
    /// Description: Converts matrix to an array.</summary>
    /// <param name="unit"></param>
    /// <returns>The matrix as a 1D array.</returns>
    member this.ToArray() =
        let arr: double array = Array.create (rows * cols) 0.0
        for i = 0 to rows - 1 do
            for j = 0 to cols - 1 do
                arr[i * cols + j] <- values[i, j]
        arr
    
    // public property to get values
    member this.Values
        with get() = values
    
    
    // public property to get rows
    member this.Rows
        with get(): int = rows

    // public property to get columns
    member this.Cols
        with get() : int = cols