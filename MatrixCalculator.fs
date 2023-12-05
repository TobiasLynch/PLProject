namespace Matrices

type MatrixCalculator =

    /// <summary>
    /// Author: Josh Mentele
    /// Description: This function adds two matrices together, returning the result. If the dimensions of the two matrices
    /// do not match, an exception is thrown.
    /// </summary>
    /// <param name="left">The first matrix to add.</param>
    /// <param name="right">The second matrix to add.</param>
    /// <returns>The resulting matrix.</returns>
    static member Add(left: Matrix, right: Matrix) : Matrix =
        if left.Rows <> right.Rows || left.Cols <> right.Cols then
            raise (System.InvalidOperationException "The dimensions of the two matrices did not match, so they cannot be added")

        // store result
        let result = new Matrix(left.Rows, left.Cols)

        // add matrix values
        for i = 0 to left.Rows - 1 do
            for j = 0 to left.Cols - 1 do
                result.Values[i, j] <- left.Values[i, j] + right.Values[i, j]
                
        // return result
        result


    /// <summary>
    /// Author: Tobias Lynch
    /// Description: This function multiplies a matrix by a scalar, returning the result.
    /// </summary>
    /// <param name="matrix">The matrix to multiply.</param>
    /// <param name="scalar">The scalar to multiply by.</param>
    /// <returns>The resulting matrix.</returns>
    static member ScalarMult(matrix: Matrix, scalar: double): Matrix =
        //Store result
        let result: Matrix = new Matrix(matrix.Rows,matrix.Cols)
        //Transform input matrix to array for easier parallelization
        let tempArr: double array = matrix.ToArray()
        
        //Multiply each value in the array by the scalar
        let arr: double array = tempArr |> Array.Parallel.map(fun x -> x * scalar)

        //Old, serial version of the function
        (*for i = 0 to matrix.Rows - 1 do
            for j = 0 to matrix.Cols - 1 do
                result.Values[i,j] <- matrix.Values[i,j] * scalar*)
                
        //Store back to matrix
        result.SetValues(arr)

        //Return result
        result

    /// <summary>
    /// Author: Tobias Lynch
    /// Description: This function multiplies one matrix by another, returning the result.
    /// </summary>
    /// <param name="left">The left matrix to multiply.</param>
    /// <param name="right">The right matrix to multiply.</param>
    /// <returns>The resulting matrix.</returns>
    static member MatrixMult(left: Matrix, right: Matrix): Matrix =
        //Verify that multiplication is possible
        if left.Cols <> right.Rows then
            raise (System.InvalidOperationException "The columns of the first matrix did not match the rows of the second, so they cannot be multiplied")
        
        let result: Matrix = new Matrix(left.Rows, right.Cols)

        for i = 0 to result.Rows - 1 do
            for j = 0 to result.Cols - 1 do
                //Get corresponding row and column
                let leftRow: double array = left.GetRow(i)
                let rightCol: double array = right.GetCol(j)
                //Multiply the corresponding elements of each array together
                let multArr: double array = Array.zip leftRow rightCol |> Array.Parallel.map(fun (x,y) -> x * y)
                //Reduce the array to one element
                let singleVal: double = multArr |> Array.reduce(fun a b -> a + b)
                //Set the position
                result.SetPos(i,j, singleVal)

        //Return result
        result