using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace ChessProject
{
    internal class Chess
    {
        static void Main ()
        {
            PrintChessboard();

            Console.WriteLine("");

            PrintArr(PawnMoves(1, 5));
        }

        
        static void PrintArr(List<int[]> arr)
        {
            for (int i = 0; i < arr.Count; i++) {
                Console.Write($"{arr[i][0]},");
                Console.WriteLine(arr[i][1]);
            }
        }


        static string[,] chessboard = {
            { "wR", "wP", " ", " ", " ", " ", "bP", "bR" },
            { "wN", "wP", " ", " ", " ", " ", "bP", "bN" },
            { "wB", "wP", " ", " ", " ", " ", "bP", "bB" },
            { "wQ", "wP", " ", " ", " ", " ", "bP", "bQ" },
            { "wK", "wP", " ", " ", " ", " ", "bP", "bK" },
            { "wB", "wP", " ", " ", " ", " ", "bP", "bB" },
            { "wN", "wP", " ", " ", " ", " ", "bP", "bN" },
            { "wR", "wP", " ", " ", " ", " ", "bP", "bR" },
        };


        static void PrintChessboard ()
        {
            Console.WriteLine("+----+----+----+----+----+----+----+----+");
            for (int i = chessboard.GetLength(0) - 1; i >= 0 ; i--) {
                for (int j = 0; j < chessboard.GetLength(1); j++) {
                    Console.Write($"| {(chessboard[j, i] == " " ? "  " : chessboard[j, i])} ");
                }
                Console.WriteLine("|");
                Console.WriteLine("+----+----+----+----+----+----+----+----+");
            }
        }


#region Piece Moves

        static void AddMoves (int startX, int startY, int deltaX, int deltaY, List<int[]> list)
        {
            char opositeColor = chessboard[startX, startY][0] == 'w' ? 'b' : 'w';
            
            int currentX = startX;
            int currentY = startY;

            while (true)
            {
                currentX += deltaX;
                currentY += deltaY;

                // Out of bounds
                if (currentX < 0 || currentX > 7 || currentY < 0 || currentY > 7) { break; }

                string coordinate = chessboard[currentX, currentY];

                if (coordinate == " ") { list.Add([currentX, currentY]); }
                else if (coordinate[0] == opositeColor) { list.Add([currentX, currentY]); break; }
                else { break; }
            }
        }


        static List<int[]> PawnMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            int direction = chessboard[x, y][0] == 'w' ? 1 : -1;
            char opositeColor = chessboard[x, y][0] == 'w' ? 'b' : 'w';

            void AddPawnMove (int x, int newY)
            {
                if (newY >= 0 && newY <= 7 && chessboard[x, newY] == " ")
                {
                    moves.Add([x, newY]);
                }
            }

            // Move one square
            int oneSqrY = y + direction;
            AddPawnMove(x, oneSqrY);

            // Move two squares
            int twoSqrY = y + 2 * direction;
            if (y == 1 && direction == 1 || y == 6 && direction == -1)
            {
                AddPawnMove(x, twoSqrY);
            }

            // Captures
            int[] capturesX = { x + 1, x - 1 };
            foreach (int captureXPos in capturesX)
            {
                if (captureXPos >= 0 && captureXPos <= 7)
                {
                    int captureYPos = oneSqrY;
                    if (chessboard[captureXPos, oneSqrY][0] == opositeColor) { moves.Add([captureXPos, captureYPos]); }
                }
            }

            return moves;
        }


        static List<int[]> RookMoves(int x, int y) {
            List<int[]> moves = new List<int[]>();

            // Add movement for vertical direction
            AddMoves(x, y, 0, 1, moves);
            AddMoves(x, y, 0, -1, moves);

            // Add movement for horizontal direction
            AddMoves(x, y, 1, 0, moves);
            AddMoves(x, y, -1, 0, moves);

            return moves;
        }


        static List<int[]> OLDWhitePawnMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            // out of bound
            if (y == 7) { return moves; }

            // pawn hasn't moved
            if (y == 1 & chessboard[x, y + 2] == " ") { moves.Add([x, y + 2]); }

            // pawn move
            if (chessboard[x, y + 1] == " ") { moves.Add([x, y + 1]); }

            // pawn capture
            if (x < 7 & chessboard[x + 1, y + 1][0] == 'b') { moves.Add([x + 1, y + 1]); }
            if (x > 0 & chessboard[x - 1, y + 1][0] == 'b') { moves.Add([x - 1, y + 1]); }

            return moves;
        }

#endregion
    }
}