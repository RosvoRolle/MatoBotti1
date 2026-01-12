using System;
using System.Diagnostics;
using System.Timers;

class Matopeli
{
    static void Main()
    {
        char[,] kartta = new char[20, 20];
        int matoX = 5;
        int matoY = 5;
        int ruokaX = 8;
        int ruokaY = 8;
        Random randomi = new Random();
        Stopwatch kello = new Stopwatch();
        kello.Start();
        double ViimeinenLiike = 0;
        double MoveInterval = 0.2f;



        
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                if (y == 0 || y == 19 || x == 0 || x == 19)
                {
                    kartta[y, x] = '#';
                }
                else
                {
                    kartta[y, x] = '.';
                }
            }
        }

        kartta[matoY, matoX] = 'O';
        kartta[ruokaY, ruokaX] = 'F';

        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
                Console.Write(kartta[y, x] + " ");
            Console.WriteLine();
        }





        void LiikutaRobotti()
        {
            int kohdeX = ruokaX;
            int kohdeY = ruokaY;


            if (matoX < kohdeX) matoX++;
            else if (matoX > kohdeX) matoX--;
            else if (matoY < kohdeY) matoY++;
            else if (matoY > kohdeY) matoY--;

        }


        
        while (true)
        {
            

            if (matoY == ruokaY &&  matoX == ruokaX)
            {
                ruokaX = randomi.Next(1, 19);
                ruokaY = randomi.Next(1, 19);
                kartta[ruokaX, ruokaY] = 'F';

                Console.SetCursorPosition(ruokaX * 2, ruokaY);
                Console.Write('F');
            }


            double aika = kello.Elapsed.TotalSeconds;
            
                if (aika - ViimeinenLiike >= MoveInterval)
                {

                Console.SetCursorPosition(matoX * 2, matoY);
                Console.Write('.');

                
                LiikutaRobotti();

                
                Console.SetCursorPosition(matoX * 2, matoY);
                Console.Write('O');

                    ViimeinenLiike = aika;
            }






           

            
            





        }

    }
}