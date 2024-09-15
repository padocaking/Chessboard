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

            TestMoves(QueenMoves(3, 3));
        }

        
        static void PrintArr(List<int[]> arr)
        {
            for (int i = 0; i < arr.Count; i++) {
                Console.Write($"{arr[i][0]},");
                Console.WriteLine(arr[i][1]);
            }
        }


        static void TestMoves(List<int[]> moves)
        {
            foreach (int[] move in moves)
            {
                int Xpos = move[0];
                int Ypos = move[1];

                chessboard[Xpos, Ypos] = "OO";
            }

            PrintChessboard();
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

        #region Piece Movement



        #endregion


        #region Piece Legal Moves

        static void AddMoves (int startX, int startY, int deltaX, int deltaY, List<int[]> list)
        {
            char opositeColor = GetOpositeColor(startX, startY);
            
            int currentX = startX;
            int currentY = startY;

            while (true)
            {
                currentX += deltaX;
                currentY += deltaY;

                // Out of bounds
                if (!IsValidPosition(currentX, currentY))
                {
                    break;
                }

                string piece = chessboard[currentX, currentY];

                if (piece == " ")
                {
                    list.Add([currentX, currentY]);
                }
                else if (piece[0] == opositeColor)
                {
                    list.Add([currentX, currentY]);
                    break;
                }
                else
                {
                    break;
                }
            }
        }


        static char GetOpositeColor(int x, int y) 
        {
            return chessboard[x, y][0] == 'w' ? 'b' : 'w';
        }


        static bool IsValidPosition (int x, int y)
        {
            return x >= 0 && x <= 7 && y >= 0 && y <= 7;
        }


        static List<int[]> PawnMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            int direction = chessboard[x, y][0] == 'w' ? 1 : -1;
            char opositeColor = GetOpositeColor(x, y);

            void AddPawnMove (int x, int newY)
            {
                if (IsValidPosition(x, newY) && chessboard[x, newY] == " ")
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


        static List<int[]> KnightMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            char opositeColor = GetOpositeColor(x, y);

            void AddKnightMove (int newX, int newY)
            {
                if (IsValidPosition(newX, newY))
                {
                    string piece = chessboard[newX, newY];
                    if (piece == " " || piece[0] == opositeColor) { moves.Add([newX, newY]); }
                }
            }

            int[,] knightMovement = {
                { -2, 1 }, { -1, 2 },
                { 1, 2 }, { 2, 1 },
                { -2, -1 }, { -1, -2 },
                { 1, -2 }, { 2, -1 },
            };

            for (int i = 0; i < knightMovement.GetLength(0); i++)
            {
                int newX = x + knightMovement[i, 0];
                int newY = y + knightMovement[i, 1];

                AddKnightMove(newX, newY);
            }

            return moves;
        }


        static List<int[]> BishopMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            // Add one diagonal moves
            AddMoves(x, y, 1, 1, moves);
            AddMoves(x, y, -1, -1, moves);

            // Add other diagonal moves
            AddMoves(x, y, -1, 1, moves);
            AddMoves(x, y, 1, -1, moves);

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


        static List<int[]> QueenMoves(int x, int y)
        {
            List<int[]> moves = new List<int[]>();

            // Add movement for vertical direction
            AddMoves(x, y, 0, 1, moves);
            AddMoves(x, y, 0, -1, moves);

            // Add movement for horizontal direction
            AddMoves(x, y, 1, 0, moves);
            AddMoves(x, y, -1, 0, moves);

            // Add one diagonal moves
            AddMoves(x, y, 1, 1, moves);
            AddMoves(x, y, -1, -1, moves);

            // Add other diagonal moves
            AddMoves(x, y, -1, 1, moves);
            AddMoves(x, y, 1, -1, moves);

            return moves;
        }

    #endregion

    }
}