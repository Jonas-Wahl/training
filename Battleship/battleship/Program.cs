using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        public static void createOpponentPitch(int x, int y, char[,] ships)
        {
            Random rand = new Random();
            int ir;
            for (int i = 0; i < x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    ir = rand.Next(1,15);
                    if(ir == 1)
                    {
                        ships[i, i2] = 's';
                    }
                    else
                    {
                        ships[i, i2] = '.';
                    }
                }
            }
        }

        public static void createOwnPitch(int x, int y, char[,] pitch)
        {
            for (int i = 0; i < x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    pitch[i, i2] = '.';
                }
            }
        }

        public static void shoot(int x, int y, char[,] pitch, char[,] ships, int posx, int posy)
        {
            if(ships[posx,posy] == 's')
            {
                pitch[posx, posy] = 'x';
                ships[posx, posy] = '.';
            }
            else
            {
                pitch[posx, posy] = 'o';
            }
        }

        public static void moveUp(int x, int y, char[,] pitch, ref int xpos, ref int ypos, int counter )
        {
            if (xpos == 0)
            {
                xpos = x - 1;
            }
            else
            {
                xpos--;
            }
            if (pitch[xpos, ypos] == 'x' || pitch[xpos, ypos] == 'o')
            {
                counter++;
                if (counter <= x )
                {
                    moveUp(x, y, pitch, ref xpos, ref ypos, counter);
                }
            }
        }

        public static void moveDown(int x, int y, char[,] pitch, ref int xpos, ref int ypos, int counter)
        {
            if (xpos == x - 1)
            {
                xpos = 0;
            }
            else
            {
                xpos++;
            }
            if (pitch[xpos, ypos] == 'x' || pitch[xpos, ypos] == 'o')
            {
                counter++;
                if (counter <= x)
                {
                    moveDown(x, y, pitch, ref xpos, ref ypos, counter);
                }
            }
        }

        public static void moveRight(int x, int y, char[,] pitch, ref int xpos, ref int ypos, int counter)
        {
            if (ypos == y - 1)
            {
                ypos = 0;
            }
            else
            {
                ypos++;
            }
            if (pitch[xpos, ypos] == 'x' || pitch[xpos, ypos] == 'o')
            {
                if (counter <= x)
                {
                    moveRight(x, y, pitch, ref xpos, ref ypos, counter);
                }
            }
        }

        public static void moveLeft(int x, int y, char[,] pitch, ref int xpos, ref int ypos, int counter)
        {
            if (ypos == 0)
            {
                ypos = y - 1;
            }
            else
            {
                ypos--;
            }
            if (pitch[xpos, ypos] == 'x' || pitch[xpos, ypos] == 'o')
            {
                counter++;
                if (counter <= x)
                {
                    moveLeft(x, y, pitch, ref xpos, ref ypos, counter);
                }
            }
        }

        public static bool getPosition(int x, int y, char[,] pitch, ref int xpos, ref int ypos, int remainingShips)
        {
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            bool moved = true;
            input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    moveUp(x, y, pitch, ref xpos, ref ypos, 0);
                    break;
                case ConsoleKey.DownArrow:
                    moveDown(x, y, pitch, ref xpos, ref ypos, 0);
                    break;
                case ConsoleKey.RightArrow:
                    moveRight(x, y, pitch, ref xpos, ref ypos, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    moveLeft(x, y, pitch, ref xpos, ref ypos, 0);
                    break;
                case ConsoleKey.Enter:
                    moved = false;
                    break;
            }
            return moved;
        }

        public static void printMarkedPosition(int x, int y, char[,] pitch, int xpos, int ypos, int remainingShips, int remainingMoves)
        {
            for (int i = 0; i < x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    if (i == xpos && i2 == ypos)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write("{0}", pitch[i, i2]);
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Verbleibende Schiffe: {0}",remainingShips);
            Console.WriteLine("Verbleibende Züge: {0}", remainingMoves);
        }

        public static void countShips(int x, int y, char[,] ships, out int count)
        {
            count = 0;
            for (int i = 0; i < x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    if(ships[i,i2] == 's')
                    {
                        count++;
                    }
                }
            }
        }

        public static int calculateMoves(int executedMoves, int maxMoves)
        {
            return maxMoves - executedMoves;
        }

        static void Main(string[] args)
        {
            int x = 10;
            int y = 20;
            char[,] pitch = new char[x, y];
            char[,] ships = new char[x, y];
            int remainingShips;
            int executedMoves = 0;
            int remainingMoves = 0;
            int maxMoves = 50;
            int xpos = 0;
            int ypos = 0;

            // create opponent
            createOpponentPitch(x, y, ships);

            // create pitch
            createOwnPitch(x, y, pitch);
            do
            {
                countShips(x, y, ships, out remainingShips);
                Console.Clear();
                remainingMoves = calculateMoves(executedMoves, maxMoves);
                printMarkedPosition(x, y, pitch, xpos, ypos, remainingShips, remainingMoves);
                if (!getPosition(x, y, pitch, ref xpos, ref ypos, remainingShips)) {
                    executedMoves++;
                    shoot(x, y, pitch, ships, xpos, ypos);
                    remainingMoves = calculateMoves(executedMoves, maxMoves);
                }
            } while (remainingMoves > 0 && remainingShips > 0);
            // pause console
            Console.ReadLine();
        }
    }
}
