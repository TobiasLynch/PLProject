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
    
    member this.SetPos(i : int, j : int, value : double) =
        if i >= rows || j >= cols then
            raise (System.InvalidOperationException "An invalid location was supplied for the matrix")
        else
            values.[i,j] <- value

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
    
    /// <summary>
    /// Author: Tobias Lynch
    /// Description: Gets a row from the matrix as an array.</summary>
    /// <param name="rowNum">The number of the row, zero-indexed</param>
    /// <returns>A row from the matrix as a 1D array.</returns
    member this.GetRow(rowNum : int): double array =
        if rowNum >= rows then
            raise (System.InvalidOperationException "The given row number is not in the matrix")
        let arr: double array = Array.create cols 0.0
        for j = 0 to cols - 1 do
            arr[j] <- values[rowNum, j]
        arr

    
    /// <summary>
    /// Author: Tobias Lynch
    /// Description: Gets a column from the matrix as an array.</summary>
    /// <param name="colNum">The number of the column, zero-indexed</param>
    /// <returns>A column from the matrix as a 1D array.</returns
    member this.GetCol(colNum : int): double array =
        if colNum >= cols then
            printfn "%d" colNum
            raise (System.InvalidOperationException "The given column number is not in the matrix")
        let arr: double array = Array.create rows 0.0
        for i = 0 to rows - 1 do
            arr[i] <- values[i, colNum]
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