namespace ChessProject
{
    internal class Chess
    {
        static void Main ()
        {
            PrintChessboard();

            Console.WriteLine("");
            PrintArr(WhitePawnMoves([1, 1]));
        }

        
        static void PrintArr(List<int[]> arr)
        {
            for (int i = 0; i < arr.Count; i++) {
                Console.Write($"{arr[i][0]},");
                Console.WriteLine(arr[i][1]);
            }
        }


        static string[,] chessboard = {
            { "wR", "wP", "", "", "", "", "bP", "bR" },
            { "wN", "wP", "", "", "", "", "bP", "bN" },
            { "wB", "wP", "", "", "", "", "bP", "bB" },
            { "wQ", "wP", "", "", "", "", "bP", "bQ" },
            { "wK", "wP", "", "", "", "", "bP", "bK" },
            { "wB", "wP", "", "", "", "", "bP", "bB" },
            { "wN", "wP", "", "", "", "", "bP", "bN" },
            { "wR", "wP", "", "", "", "", "bP", "bR" },
        };


        static void PrintChessboard ()
        {
            Console.WriteLine("+----+----+----+----+----+----+----+----+");
            for (int i = chessboard.GetLength(0) - 1; i >= 0 ; i--) {
                for (int j = 0; j < chessboard.GetLength(1); j++) {
                    Console.Write($"| {(chessboard[j, i] == "" ? "  " : chessboard[j, i])} ");
                }
                Console.WriteLine("|");
                Console.WriteLine("+----+----+----+----+----+----+----+----+");
            }
        }


#region Piece Legal Moves

        static List<int[]> WhitePawnMoves(int[] coord)
        {
            List<int[]> moves = new List<int[]>();

            bool moved = coord[1] != 1;

            try { 
                if (!moved) {
                    for (int i = 1; i <= 2; i++) {
                        if (chessboard[coord[0], coord[1] + i] == "") {
                            moves.Add([coord[0], coord[1] + i]);
                        }
                    }
                }
                else {
                    if (chessboard[coord[0], coord[1] + 1] == "") {
                        moves.Add([coord[0], coord[1] + 1]);
                    }
                }

                for (int i = -1; i <= 1; i += 2) {
                    if (chessboard[coord[0] + i, coord[1] + 1][0] == 'b') {
                        moves.Add([coord[0] + i, coord[1] + 1]);
                    }
                }

            } catch {};

            return moves;
        }


        static List<int[]> BlackPawnMoves(int[] coord)
        {
            List<int[]> moves = new List<int[]>();

            bool moved = coord[1] != 6;

            try {
                if (!moved) {
                    for (int i = 1; i <= 2; i++) {
                        if (chessboard[coord[0], coord[1] - i] == "") {
                            moves.Add([coord[0], coord[1] - i]);
                        }
                    }
                }
                else {
                    if (chessboard[coord[0], coord[1] - 1] == "")
                    {
                        moves.Add([coord[0], coord[1] - 1]);
                    }
                }

                for (int i = -1; i <= 1; i += 2) {
                    if (chessboard[coord[0] + i, coord[1] - 1][0] == 'w') {
                        moves.Add([coord[0] + i, coord[1] - 1]);
                    }
                }

            }
            catch {};

            return moves;
        }

#endregion
    }
}