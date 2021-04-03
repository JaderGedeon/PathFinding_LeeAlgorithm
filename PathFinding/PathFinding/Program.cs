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

            int[][] positionsCoordinates = new int[][] {
                new int[] { 0 ,-1 },
                new int[] { 1 , 0 },
                new int[] { 0 , 1 },
                new int[] { -1, 0 }
            };

            int[] finalPoint = FindFinalPoint(queuePoints, positionsCoordinates, board);

            Queue queueWayToPointA = new Queue(board.GetLength(0) * board.GetLength(0));

            queueWayToPointA.Enqueue(finalPoint);


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

            Console.WriteLine(finalPoint[0] != -1
                                ? WayToFinalPoint(queueWayToPointA, positionsCoordinates, board)
                                : "Não há caminhos do ponto A ao ponto B");
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

        private static int[] FindFinalPoint(Queue queuePoints, int[][] positionsCoordinates, int[][] board)
        {
            while (!queuePoints.IsEmpty()) // if
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
                                return new int[] { summedCoordinate[0], summedCoordinate[1] };
                        }
                    }
                }
                return FindFinalPoint(queuePoints, positionsCoordinates, board);
            }
            return new int[] { -1, -1 };
        }

        private static string WayToFinalPoint(Queue queueWay, int[][] positionsCoordinates, int[][] board)
        {

            string wayToPointA = "";

            bool isFindingA = true;

            while (isFindingA)
            {
                int[] selectedPoint = queueWay.Dequeue();

                int lessValueArountThePoint = 0;
                int directionToA = 0;

                for (int i = 0; i < 4; i++)
                {
                    int[] summedCoordinate = new int[2] { selectedPoint[0] + positionsCoordinates[i][0],
                                                          selectedPoint[1] + positionsCoordinates[i][1] };

                    if (summedCoordinate[0] >= 0
                        && summedCoordinate[0] < board.GetLength(0)
                        && summedCoordinate[1] >= 0
                        && summedCoordinate[1] < board.GetLength(0))
                    {
                        if (board[summedCoordinate[0]][summedCoordinate[1]] == -2)
                            isFindingA = false;

                        if ((board[summedCoordinate[0]][summedCoordinate[1]] < lessValueArountThePoint || lessValueArountThePoint == 0) 
                            && board[summedCoordinate[0]][summedCoordinate[1]] > 0 || board[summedCoordinate[0]][summedCoordinate[1]] == -2)
                        {
                            lessValueArountThePoint = board[summedCoordinate[0]][summedCoordinate[1]];
                            directionToA = i;
                        }
                    }
                }

                switch (directionToA)
                {
                    case 0:
                        wayToPointA += "Direita ";
                        break;

                    case 1:
                        wayToPointA += "Cima ";
                        break;

                    case 2:
                        wayToPointA += "Esquerda ";
                        break;

                    case 3:
                        wayToPointA += "Baixo ";
                        break;
                }



                queueWay.Enqueue(new int[2] { selectedPoint[0] + positionsCoordinates[directionToA][0],
                                              selectedPoint[1] + positionsCoordinates[directionToA][1] });


            }

            string[] directionArray = wayToPointA.Split(' ');

            string revertedWayToPointA = "";

            for (int i = directionArray.Length - 1; i >= 0; i--)
            {
                revertedWayToPointA += directionArray[i] + "\n";

            }

            return revertedWayToPointA;
        }
    }
}
