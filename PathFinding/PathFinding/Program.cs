/*
Entrega de trabalho
Nós,

Jader Gedeon De Oliveira Rocha
Matheus Baltazar Ramos

declaramos que
todas as respostas são fruto de nosso próprio trabalho,
não copiamos respostas de colegas externos à equipe,
não disponibilizamos nossas respostas para colegas externos à equipe e
não realizamos quaisquer outras atividades desonestas para nos beneficiar
ou prejudicar outros.
*/

using System;

namespace PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {

            int[][] board = ReadBoardArchive();
            int[] initialPoint = FindInitialPoint(board);

            Queue queuePoints = new Queue(board.GetLength(0) * board.GetLength(0));

            queuePoints.Enqueue(initialPoint);

            bool pointFounded = false;

            int[][] positionsCoordinates = new int[][] {
                new int[] { 0,-1 },
                new int[] { 1,0 },
                new int[] { 0,1 },
                new int[] { -1,0 }
            };

            while (!queuePoints.IsEmpty() && pointFounded == false)
            {
                int[] selectedPoint = queuePoints.Dequeue();

                foreach (int[] positionCoord in positionsCoordinates)
                {
                    int[] summedCoordinate = new int[2] { selectedPoint[0] + positionCoord[0],
                                                          selectedPoint[1] + positionCoord[1] };

                    if (summedCoordinate[0] >= 0
                        && summedCoordinate[0] < board.GetLength(0)
                        && summedCoordinate[1] >= 0
                        && summedCoordinate[1] < board.GetLength(0))
                    {

                        switch (board[summedCoordinate[0]][summedCoordinate[1]])
                        {
                            case 0:
                                board[summedCoordinate[0]][summedCoordinate[1]] = board[selectedPoint[0]][selectedPoint[1]] == -2
                                    ? 1 : board[selectedPoint[0]][selectedPoint[1]] + 1;

                                queuePoints.Enqueue(summedCoordinate);
                                break;
                            case -3:
                                pointFounded = true;
                                break;
                        }
                    }
                }
            }

            string oie = "";

            for (int i = 0; i < board.GetLength(0); i++)
            {

                oie += "\n ";

                for (int j = 0; j < board.GetLength(0); j++)
                {
                    oie += "[" + board[i][j] + "] ";
                }
            }

            Console.WriteLine(oie);
        }

        private static int[][] ReadBoardArchive()
        {
            string[] linesReaded = System.IO.File.ReadAllLines(@"Board.txt");

            int[][] boardArchive = new int[linesReaded.Length][];

            for (int line = 0; line < linesReaded.Length; line++)
            {
                boardArchive[line] = Array.ConvertAll(linesReaded[line].Split(' '),
                        element => int.Parse(element));
            }
            return boardArchive;
        }

        private static int[] FindInitialPoint(int[][] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    // Return the Initial Point
                    if (board[j][i] == -2)
                        return new int[2] { j, i };
                }
            }

            throw new Exception("Não há ponto inicial.");
        }

        private static void FindFinalPoint()
        {



        }
    }
}
