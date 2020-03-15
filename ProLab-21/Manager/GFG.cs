﻿using System;
using System.Collections.Generic;

public class GFG
{

    public static readonly int NO_PARENT = -1;
    public List<Int32> kenarlar = new List<int>();
    public List<Int32> arananSehirMesafeleri = new List<int>();
    public int enKisaMesafe = 0;
    public int toplamMinMesafe = 0;
    public List<Int32> tamYol = new List<int>();
    public bool kont= true;

    public GFG()
    {
        kenarlar.Clear();
        arananSehirMesafeleri.Clear();
        enKisaMesafe = 0;
        toplamMinMesafe = 0;
        tamYol.Clear();
        kont = true;

    }

    public void topluDijikstra(int[,] komsulukMatrisi, int baslangicKenari, List<Int32> bitisKenarlari)
    {
        int minMesafe = Int32.MaxValue;
        int minMesafeIndis = Int32.MaxValue;
        for (int i = 0; i < bitisKenarlari.Count; i++)
        {
            dijkstra(komsulukMatrisi, baslangicKenari, bitisKenarlari[i]);
            if (arananSehirMesafeleri[i] < minMesafe)
            {
                minMesafe = arananSehirMesafeleri[i];
                minMesafeIndis = i;
            }
        }
        if (bitisKenarlari.Count == 0)
        {

            /*
            Console.WriteLine("\t\t Toplam Min Mesafe: " + toplamMinMesafe+ "\n");
            Console.Write("Kenar Yolu \n");
            Console.WriteLine("40");
            */
            for (int i = 0; i< tamYol.Count;i++)
            {
              //  Console.WriteLine(tamYol[i] + " ");
            }
            kont = true;
            return;
        }
        else { 
        kenarlar.Clear();
        dijkstra(komsulukMatrisi, baslangicKenari, bitisKenarlari[minMesafeIndis]);
        for (int i = 1; i < kenarlar.Count; i++)
        {
            tamYol.Add(kenarlar[i]);
        }
        //Console.WriteLine("\nMin Mesafe " + minMesafe);
        toplamMinMesafe += minMesafe;
        baslangicKenari = bitisKenarlari[minMesafeIndis];
        bitisKenarlari.RemoveAt(minMesafeIndis);
        arananSehirMesafeleri.Clear();
            if(bitisKenarlari.Count == 0 && kont == true)
            {
                bitisKenarlari.Add(40);
                kont = false;
            }
        topluDijikstra(komsulukMatrisi, baslangicKenari, bitisKenarlari);
        }


    }

    internal void topluDijikstra(object kMatris, int v, object arananListesiIndis)
    {
        throw new NotImplementedException();
    }

    public  void dijkstra(int[,] komsulukMatrisi,int baslangicKenari,int bitisKenari)
    {
        int nKenar = komsulukMatrisi.GetLength(0);
        int[] enKisaMesafeler = new int[nKenar];
        bool[] eklenenler = new bool[nKenar];

        for (int sehirIndex = 0; sehirIndex < nKenar;
                                            sehirIndex++)
        {
            enKisaMesafeler[sehirIndex] = int.MaxValue;
            eklenenler[sehirIndex] = false;
        }

        enKisaMesafeler[baslangicKenari] = 0;

        int[] parents = new int[nKenar];

        parents[baslangicKenari] = NO_PARENT;

        for (int i = 1; i < nKenar; i++)
        {

            int enYakinKenar = -1;
            int EnKisaMesafe = int.MaxValue;
            for (int sehirIndex = 0;
                    sehirIndex < nKenar;
                    sehirIndex++)
            {
                if (!eklenenler[sehirIndex] &&
                    enKisaMesafeler[sehirIndex] <
                    EnKisaMesafe)
                {
                    enYakinKenar = sehirIndex;
                    EnKisaMesafe = enKisaMesafeler[sehirIndex];
                }
            }

            eklenenler[enYakinKenar] = true;

            for (int sehirIndex = 0;
                    sehirIndex < nKenar;
                    sehirIndex++)
            {
                int kenarMesafesi = komsulukMatrisi[enYakinKenar, sehirIndex];

                if (kenarMesafesi > 0
                    && ((EnKisaMesafe + kenarMesafesi) <
                        enKisaMesafeler[sehirIndex]))
                {
                    parents[sehirIndex] = enYakinKenar;
                    enKisaMesafeler[sehirIndex] = EnKisaMesafe +
                                                    kenarMesafesi;
                }
            }
        }

        cozumuEkranaYaz(baslangicKenari, enKisaMesafeler, parents,bitisKenari);
    }
    public  void cozumuEkranaYaz(int baslangicKenari,
                                    int[] mesafeler,
                                    int[] parents,
                                    int bitisKenari)
    {
        int nKenar = mesafeler.Length;
     

        for (int sehirIndex = 0; sehirIndex < nKenar; sehirIndex++)
        {
            if (sehirIndex != baslangicKenari && sehirIndex == bitisKenari)
            {
                /*
                Console.Write("\n" + baslangicKenari + " -> ");
                Console.Write(bitisKenari + " \t\t ");
                Console.Write(mesafeler[bitisKenari] + "\t\t");
                */
                arananSehirMesafeleri.Add(mesafeler[bitisKenari]);
                cozumYolunuEkranaYaz(sehirIndex, parents);
            }
        }
    }

    public  void cozumYolunuEkranaYaz(int gecerliKenar,int[] parents)
    {
        if (gecerliKenar == NO_PARENT)
        {
            return;
        }
        cozumYolunuEkranaYaz(parents[gecerliKenar], parents);
       // Console.Write(gecerliKenar + " ");
        kenarlar.Add(gecerliKenar);

    }
}