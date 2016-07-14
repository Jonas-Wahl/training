using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        public static void print(int x,int y, char[,] pitch)
        {
            for (int i = 0; i < x; i++)
            {
                for (int i2 = 0; i2 < y; i2++)
                {
                    Console.Write("{0}", pitch[i, i2]);
                }
                Console.WriteLine("");
            }
        }
        public static void createOpponenPitch(int x, int y, char[,] ships)
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
            }
            else
            {
                pitch[posx, posy] = 'o';
            }
        }

        public static void getPosition(int x, int y, char[,] pitch, ref int xpos, ref int ypos)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                switch (key.Key)
                {
                    case: ConsoleKey.UpArrow

                        break;
                }
            } while (key.Key != ConsoleKey.Enter);
        }

        static void Main(string[] args)
        {
            int x = 10;
            int y = 20;
            char[,] pitch = new char[x,y];
            char[,] ships = new char[x, y];
            bool won = false;
            int move = 0;
            
            // create opponent
            createOpponenPitch(x, y, ships);

            // create pitch
            createOpponenPitch(x, y, pitch);
            do
            {
                move++;
                print(x, y, pitch);
                Console.WriteLine("");
                
                print(x, y, ships);
            } while (move < 20 && !won);
            // pause console
            Console.ReadLine();
        }
    }
}
