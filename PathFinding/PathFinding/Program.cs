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
using System.Collections.Generic;

namespace PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            // Definição das variáveis iniciais 
            int[][] board = ReadBoardArchive();
            Queue queuePoints = new Queue(board.GetLength(0) * board.GetLength(0));
            Queue queueWayToPointA = new Queue(board.GetLength(0) * board.GetLength(0));
            List<int[]> wayCoordinates = new List<int[]>();

            // Encontra o ponto inicial em todo o tabuleiro
            int[] initialPoint = FindInitialPoint(board);
            queuePoints.Enqueue(initialPoint);

            PrintBoard(board, wayCoordinates);

            // Vetor de vetores que armazenam as direções conforme testa os vizinhos
            int[][] positionsCoordinates = new int[][] {
                new int[] { 0 ,-1 },
                new int[] { 1 , 0 },
                new int[] { 0 , 1 },
                new int[] { -1, 0 }
            };

            // Encontra o ponto final utilizando o algoritmo de Lee
            int[] finalPoint = FindFinalPoint(queuePoints, positionsCoordinates, board);
            queueWayToPointA.Enqueue(finalPoint);

            // Exibe o caminho para chegar até o valor de B
            Console.WriteLine(finalPoint[0] != -1
                                ? "Siga o seguinte caminho para ir de A até B: \n\n" + WayToFinalPoint(queueWayToPointA, positionsCoordinates, board, wayCoordinates)
                                : "Não há caminhos do ponto A ao ponto B");

            PrintBoard(board, wayCoordinates);
        }

        // Responsável por ler o arquivo txt e armazená-lo numa matriz, que em seguida é retornada.   
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

        // Função que roda toda a matriz e retorna o ponto inicial.
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

        // Função que procura pelo ponto de B, verificando os vizinhos e incrementando os seus respectivos
        // valores, num efeito de onde do algoritmo de Lee, até encontrar o ponto B, retornando o ponto final.
        private static int[] FindFinalPoint(Queue queuePoints, int[][] positionsCoordinates, int[][] board)
        {
            if (!queuePoints.IsEmpty())
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

        // Função que realiza o cmainho de retorno de B para A, procurando pelo menor número próximo do ponto indicado, armazenando
        // essas coordenadas na variável wayCoorinates para futuramente serem exibidas.
        private static string WayToFinalPoint(Queue queueWay, int[][] positionsCoordinates, int[][] board, List<int[]> wayCoordinates)
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
                        wayToPointA += "Direita > ";
                        break;

                    case 1:
                        wayToPointA += "Cima > ";
                        break;

                    case 2:
                        wayToPointA += "Esquerda > ";
                        break;

                    case 3:
                        wayToPointA += "Baixo > ";
                        break;
                }

                int[] coordinate = new int[2] { selectedPoint[0] + positionsCoordinates[directionToA][0],
                                              selectedPoint[1] + positionsCoordinates[directionToA][1] };

                wayCoordinates.Add(coordinate);
                queueWay.Enqueue(coordinate);

            }

            string[] directionArray = wayToPointA.Split(' ');

            string revertedWayToPointA = "";

            for (int i = directionArray.Length - 1; i >= 0; i--)
            {
                revertedWayToPointA += directionArray[i] + " ";

            }

            return revertedWayToPointA.Remove(0, 3);
        }

        // Função que exibe utilizando um sistema de cores a matriz na tela do usuário, retorna o caminho na cor amarela
        // caso já tenha encontrado o mesmo.
        private static void PrintBoard(int[][] board, List<int[]> wayCoordinates)
        {

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" \n");
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    int value = board[i][j];
                    var stringAux = "";

                    switch (value)
                    {
                        case -1:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            break;
                        case -2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case -3:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case int valueTested when valueTested >= 0 && valueTested <= 9:
                            stringAux = " ";
                            break;
                        default:
                            break;
                    }

                    for (int k = 0; k < wayCoordinates.Count - 1; k++)
                    {
                        if (wayCoordinates[k][0] == i && wayCoordinates[k][1] == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        }
                    }

                    Console.Write("[" + stringAux + value + "] ");
                    Console.ResetColor();

                }
            }
            Console.Write(" \n\n");
        }
    }
}