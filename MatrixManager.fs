module Manager

open System
open Matrices

type MatrixManager() =
    // holds list of matrices
    let mutable matrices : Matrix array = [||]


    /// <summary>
    /// Author: Josh Mentele
    /// Description: This function prints all matrices currently stored.
    /// </summary>
    member this.PrintMatrices() : unit =
        // loop through matrices, print out to screen
        for i = 0 to (Array.length matrices) - 1 do
            printfn "Matrix #%d; Rows: %d, Cols: %d\n%s\n" i matrices[i].Rows matrices[i].Cols (matrices[i].ToString())


    /// <summary>
    /// Author: Josh Mentele
    /// Description: This function prints the matrix at the associated index provided. If it does not exist, an exception is thrown.
    /// </summary>
    /// <param name="idx">The index of the matrix to print.</param>
    member this.PrintMatrix(idx: int) : unit =
        // check that the matrix exists
        if idx >= matrices.Length then
            raise (InvalidOperationException "The matrix does not exist")
        
        // print out the matrix
        printfn "%s" (matrices[idx].ToString())


    /// <summary>
    /// Author: Josh Mentele
    /// Description: This function adds the two matrices at the given indices together. If either doesn't
    /// exist, an exception is thrown. The resulting matrix is stored in the list of matrices, and returned.
    /// </summary>
    /// <param name="idx1">The index of the first matrix to add.</param>
    /// <param name="idx2">The index of the second matrix to add.</param>
    /// <returns>The resulting matrix.</returns>
    member this.AddMatrices(idx1: int, idx2: int): Matrix = 
        
        // check that the two matrices exist
        if (Array.length matrices) <= idx1 || (Array.length matrices) <= idx2 then
            raise (System.InvalidOperationException "Cannot add the two matices because at least one does not exist")

        // perform operation
        let result = MatrixCalculator.Add(matrices[idx1], matrices[idx2])

        // store result
        matrices <- Array.append matrices [|result|]

        // return result
        result


    /// <summary>
    /// Author: Josh Mentele
    /// Description: This function creates a matrix with the given dimensions and values, and stores the result.
    /// </summary>
    /// <param name="rows">The number of rows.</param>
    /// <param name="cols">The number of columns.</param>
    /// <param name="values">The values of the matrix.</param>
    member this.CreateMatrix(rows: int, cols: int, values: double array) : unit =
        // create matrix
        let m = new Matrix(rows, cols)
        
        // set its values
        m.SetValues values

        // add to list
        matrices <- Array.append matrices [|m|]


    /// <summary>
    /// Author: Tobias Lynch
    /// Description: This function multiplies a given matrix by a given scalar and stores the result.
    /// </summary>
    /// <param name="id">The index of the matrix to be multiplied.</param>
    /// <param name="scalar">The scalar to multiply by.</param>
    /// <returns>The resulting matrix.</returns>
    member this.ScalarMult(id : int, scalar : double): Matrix =

        //Check matrix exists
        if (Array.length matrices) <= id then
            raise (System.InvalidOperationException "Cannot multiply the matrix as it does not exist")
        
        //Perform operation
        let result: Matrix = MatrixCalculator.ScalarMult(matrices[id], scalar)

        //Store result
        matrices <- Array.append matrices [|result|]

        //Return result
        result