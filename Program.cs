using System;
using System.Collections.Generic;
using System.Diagnostics;

class Node
{
    public int xPos, yPos;
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Node(int x, int y)
    {
        xPos = x;
        yPos = y;
        gCost = 0;
        hCost = 0;
        parent = null;
    }
}

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

        
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                if (y == 0 || y == 19 || x == 0 || x == 19)
                    kartta[y, x] = '#';
                else
                    kartta[y, x] = '.';
            }
        }

        
        for (int x = 3; x <= 15; x++)
            kartta[10, x] = '#';
        for (int y = 3; y <= 15; y++)
            kartta[y, 7] = '#';

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
            openList.Clear();
            closedList.Clear();

            
            Node matoNode = new Node(matoX, matoY);
            matoNode.gCost = 0;
            matoNode.hCost = Math.Abs(matoX - ruokaX) + Math.Abs(matoY - ruokaY);
            openList.Add(matoNode);

            Node kohdeNode = null;

            
            while (openList.Count > 0)
            {
                
                Node nyky = openList[0];
                foreach (Node n in openList)
                {
                    if (n.fCost < nyky.fCost || (n.fCost == nyky.fCost && n.hCost < nyky.hCost))
                        nyky = n;
                }

                openList.Remove(nyky);
                closedList.Add(nyky);

                
                if (nyky.xPos == ruokaX && nyky.yPos == ruokaY)
                {
                    kohdeNode = nyky;
                    break;
                }

               
                int[,] suunnat = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
                for (int i = 0; i < 4; i++)
                {
                    int uusiX = nyky.xPos + suunnat[i, 0];
                    int uusiY = nyky.yPos + suunnat[i, 1];

                    if (uusiX < 0 || uusiY < 0 || uusiX >= 20 || uusiY >= 20)
                        continue;
                    if (kartta[uusiY, uusiX] == '#')
                        continue;
                    if (closedList.Exists(n => n.xPos == uusiX && n.yPos == uusiY))
                        continue;

                    Node naapuri = new Node(uusiX, uusiY);
                    naapuri.gCost = nyky.gCost + 1;
                    naapuri.hCost = Math.Abs(uusiX - ruokaX) + Math.Abs(uusiY - ruokaY);
                    naapuri.parent = nyky;

                    if (!openList.Exists(n => n.xPos == naapuri.xPos && n.yPos == naapuri.yPos))
                        openList.Add(naapuri);
                }
            }

            
            if (kohdeNode != null)
            {
                List<Node> polku = new List<Node>();
                Node nykynen = kohdeNode;
                while (nykynen != null)
                {
                    polku.Add(nykynen);
                    nykynen = nykynen.parent;
                }
                polku.Reverse();

                if (polku.Count > 1) 
                {
                    matoX = polku[1].xPos;
                    matoY = polku[1].yPos;
                }
            }
        }

        
        while (true)
        {
            if (matoY == ruokaY && matoX == ruokaX)
            {
                ruokaX = randomi.Next(1, 19);
                ruokaY = randomi.Next(1, 19);
                kartta[ruokaY, ruokaX] = 'F';
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