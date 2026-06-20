using System;
using System.Collections.Generic;
using System.Text;

namespace Hashing
{
    
    class Program
    {

        

        static void Main(string[] args)
        {
            int tabloBoyutu = 11;
            int i=1;
            int ortLISCH = 0, ortPO = 0, ortLQ = 0;
           
            int[] tablo_LISCH = new int[tabloBoyutu];
            int[] tablo_LISCH_LINK = new int[tabloBoyutu];
            int[] tablo_PO = new int[tabloBoyutu];
            int[] tablo_LQ = new int[tabloBoyutu];            
            
            int secim;
            int sayi;
            TabloYenile(tablo_LISCH);
            TabloYenile(tablo_PO);
            TabloYenile(tablo_LQ);
            TabloYenile(tablo_LISCH_LINK);

            menuGoster();
            secim = Convert.ToInt16(Console.ReadLine());
            while (true)
            {
                switch (secim)
                {
                    case 1:
                        while (i < tabloBoyutu + 1)
                        {
                            sayi = System.Convert.ToInt32(Console.ReadLine());
                            ortLISCH += LISCH(sayi, "ekle", tablo_LISCH, tablo_LISCH_LINK);
                            ortPO += ProgressiveOverflow(sayi, "ekle", tablo_PO);
                            ortLQ += LinearQuotient(sayi, "ekle", tablo_LQ);
                            Console.WriteLine("Ortalama Adim Sayilari : \nLISCH = {0}\nPO = {1}\nLQ = {2}", (float)ortLISCH / (float)i, ((float)ortPO / (float)i), ((float)ortLQ / (float)i));
                            i++;
                        }
                        Console.WriteLine("\n\nTABLOLAR DOLDU SONUCLAR :");
                        Console.WriteLine("Ortalama Adim Sayilari : \nLISCH = {0}\nPO = {1}\nLQ = {2}", ((float)ortLISCH / (float)(i - 1)), ((float)ortPO / (float)(i - 1)), ((float)ortLQ / (float)(i - 1)));
                        menuGoster();
                        secim = Convert.ToInt16(Console.ReadLine());
                        break;
                    case 2:
                        sayi = System.Convert.ToInt32(Console.ReadLine());
                        ortLISCH += LISCH(sayi, "ara", tablo_LISCH, tablo_LISCH_LINK);
                        ortPO += ProgressiveOverflow(sayi, "ara", tablo_PO);
                        ortLQ += LinearQuotient(sayi, "ara", tablo_LQ);
                        Console.WriteLine("Ortalama Adim Sayilari : \nLISCH = {0}\nPO = {1}\nLQ = {2}", (float)ortLISCH / (float)i, ((float)ortPO / (float)i), ((float)ortLQ / (float)i));
                        break;
                    default:
                        Console.WriteLine("yanlis secim yaptiniz.");
                        break;

                }
            }

        }

        public static int LISCH(int sayi, string mod, int[] tablo,int[] linkTable)
        {
            int H = Hash_1(sayi, tablo.Length); // H de�i�keninin i�ine hashle bizi spak
            int adimSayisi = 1;
            if (mod == "ekle") // ekle modu
            {

                if (tablo[H] == -1) // tablonun h. eleman� bo�sa ekle
                {
                    tablo[H] = sayi;
                    Console.WriteLine("{0} collision olmadan {1} adimda tabloya eklendi... (LISCH)", sayi, adimSayisi);
                    return adimSayisi;
                }

                else if (tablo[H] == sayi)
                {
                    Console.WriteLine("{0} zaten tabloda var... (LISCH)", sayi);
                    return adimSayisi;
                }
                else //bo� de�ilse �ak��ma vard�r burdan yak
                {
                    while (true)
                    {
                        if (linkTable[H] == -1)
                        {
                            int say = tablo.Length - 1;

                            while (tablo[say] != -1)//tablonun say. eleman� -1 olana kadar say� azalt
                            {
                                adimSayisi++;
                                say--;
                                if (say == -1)// say tablonun d���na ��km��sa olay bitmi�tir.
                                {
                                    Console.WriteLine("Tablo dolu... (LISCH)");
                                    return 0;
                                }
                            }

                            tablo[say] = sayi;//ve nihayet...
                            linkTable[H] = say;

                            Console.WriteLine("{0} collision cozulerek {1} adimda tabloya eklendi... (LISCH)", sayi, adimSayisi);
                            return adimSayisi;

                        }

                        else 
                        {
                            H = linkTable[H];
                        }

                    }

                }

            }

            else if (mod == "ara") // arama modu
            {
                int home = H;
                do
                {
                    adimSayisi++;
                    if (tablo[H] == sayi) // arad���m�z yerde bulduk
                    {

                        Console.WriteLine("{0} elemani tablonun {1}. satirinda {2} adimda bulunuverdi (LISCH)", sayi, H, adimSayisi);
                        return adimSayisi;
                    }
                    else
                    {
                        H = linkTable[H];
                        if (H == -1)
                        {
                            Console.WriteLine("{0} elemani bulunamadi({1} adimda) ..uzulmeyin size eleman mi yok..(LISCH)", sayi, adimSayisi); // arad���n�z ki�iye �u anda ula��lam�yor.
                            return 0;
                        }
                    }
                } while (H != home);

                Console.WriteLine("{0} elemani bulunamadi({1} adimda) ..uzulmeyin size eleman mi yok..(LISCH)", sayi, adimSayisi); // arad���n�z ki�iye �u anda ula��lam�yor.
                return 0;
                

            }

            else
            {
                Console.WriteLine("olmayacak isler pesindesiniz."); // yanl�� mod se�ilmi� fatal error
                return 0;
            }

            

        }// LISCH SON ---------------

        //----------------------------------------------------------------------------------------

        public static int ProgressiveOverflow(int sayi, string mod, int[] tablo)
        {
            int H = Hash_1(sayi, tablo.Length); // H de�i�keninin i�ine hashle bizi spak
            int adimSayisi = 1;
            if (mod == "ekle") // ekle modu
            {

                if (tablo[H] == -1) // tablonun h. eleman� bo�sa ekle
                {
                    tablo[H] = sayi;
                    Console.WriteLine("{0} collision olmadan {1} adimda tabloya eklendi... (PO)", sayi,adimSayisi);
                    return adimSayisi;
                }

                else if (tablo[H] == sayi)
                {
                    Console.WriteLine("{0} zaten tabloda var... (PO)", sayi);
                    return 0;
                }
                else //bo� de�ilse �ak��ma vard�r burdan yak
                {
                    int sayici = (H+1)%tablo.Length;
                    while (sayici != H)
                    {
                        adimSayisi++;
                        if (tablo[sayici] == sayi) // bu eleman zaten varm�� 
                        {
                            Console.WriteLine("{0} zaten tabloda var... (PO)", sayi);
                            return 0;
                        }
                        if (tablo[sayici] == -1) //�ak��ma ��z�ld� g�z�m�z ayd�n
                        {
                            tablo[sayici] = sayi;
                            Console.WriteLine("{0} collision cozulerek {1} adimda tabloya eklendi... (PO)", sayi, adimSayisi);
                            return adimSayisi;
                        }

                        sayici++; // say�c�y� kutsa 
                        if (sayici == tablo.Length)
                        {
                            sayici = 0;
                        }
                    }
                    Console.WriteLine("Tablo dolu... (PO)");
                }

            }

            else if (mod == "ara") // arama modu
            {
                if (tablo[H] == sayi) // arad���m�z yerde bulduk
                {
                    Console.WriteLine("{0} elemani tablonun {1}. satirinda {2} adimda bulunuverdi (PO)", sayi, H, adimSayisi);
                    return adimSayisi;
                }
                int sayici = (H+1)%tablo.Length;

                while (sayici != H)
                {
                    adimSayisi++;
                    if (tablo[sayici] == sayi)// g�kte ararken yerde bulduk..
                    {
                        Console.WriteLine("{0} elemani tablonun {1}. satirinda {2} adimda bulunuverdi (PO)", sayi, sayici, adimSayisi);
                        return adimSayisi;
                    }
                    sayici++;// say�c� g��e do�ru
                    if (sayici == tablo.Length)
                    {
                        sayici = 0;
                    }
                }

                Console.WriteLine("{0} elemani bulunamadi({1} adim sonucu) ..uzulmeyin size eleman mi yok..(PO)", sayi,adimSayisi); // arad���n�z ki�iye �u anda ula��lam�yor.
                return 0;

            }

            else
            {
                Console.WriteLine("olmayacak isler pesindesiniz."); // yanl�� mod se�ilmi� fatal error
                return 0;
            }

            return adimSayisi;
 
        }// Progressive SON --------

        //----------------------------------------------------------------------------------------

        public static int LinearQuotient(int sayi, string mod, int[] tablo)
        {
            int H = Hash_1(sayi, tablo.Length); // H de�i�keninin i�ine hashle bizi spak
            int adim = Hash_2(sayi, tablo.Length);// ikinci hashhash
            int adimSayisi = 1;

            if (mod == "ekle") // ekle modu
            {

                if (tablo[H] == -1) // tablonun h. eleman� bo�sa ekle
                {
                    tablo[H] = sayi;
                    Console.WriteLine("{0} collision olmadan {1} adimda tabloya eklendi... (LQ)", sayi,adimSayisi);
                    return adimSayisi;
                }

                else if (tablo[H] == sayi)
                {
                    Console.WriteLine("{0} zaten tabloda var... (LQ)", sayi);
                    return 0;
                }
                else //bo� de�ilse �ak��ma vard�r burdan yak
                {
                    int sayici = (H+adim)%tablo.Length;
                    while (sayici != H)
                    {
                        adimSayisi++;
                        if (tablo[sayici] == sayi) // bu eleman zaten varm�� 
                        {
                            Console.WriteLine("{0} zaten tabloda var... (LQ)", sayi);
                            return 0;
                        }
                        if (tablo[sayici] == -1) //�ak��ma ��z�ld� g�z�m�z ayd�n
                        {
                            tablo[sayici] = sayi;
                            Console.WriteLine("{0} collision cozulerek {1} adimda tabloya eklendi... (LQ)", sayi, adimSayisi);
                            return adimSayisi;
                        }

                        sayici+=adim; // say�c�yla ad�m� evlendir
                        if (sayici >= tablo.Length)
                        {
                            sayici = sayici%tablo.Length;
                        }
                    }
                    Console.WriteLine("Tablo dolu... (LQ)");
                }

            }

            else if (mod == "ara") // arama modu
            {
                if (tablo[H] == sayi) // arad���m�z yerde bulduk
                {
                    Console.WriteLine("{0} elemani tablonun {1}. satirinda {2} adimda bulunuverdi (LQ)", sayi, H, adimSayisi);
                    return adimSayisi;
                }
                int sayici = (H + adim) % tablo.Length;

                while (sayici != H)
                {
                    adimSayisi++;
                    if (tablo[sayici] == sayi)// g�kte ararken yerde bulduk..
                    {
                        Console.WriteLine("{0} elemani tablonun {1}. satirinda {2} adimda bulunuverdi (LQ)", sayi, sayici, adimSayisi);
                        return adimSayisi;
                    }
                    sayici+=adim;// say�c�yla ad�m kayna��n
                    if (sayici >= tablo.Length)
                    {
                        sayici = sayici%tablo.Length;
                    }
                }

                Console.WriteLine("{0} elemani bulunamadi({1} adim sonunda)..uzulmeyin size eleman mi yok..(LQ)", sayi, adimSayisi); // arad���n�z ki�iye �u anda ula��lam�yor.
                return 0;

            }

            else
            {
                Console.WriteLine("olmayacak isler pesindesiniz."); // yanl�� mod se�ilmi� fatal error
                return 0;
            }
            return adimSayisi;
 
        }//LQ SON ---------

        //----------------------------------------------------------------------------------------

        // AMELE FONKS�YONLAR --------------------------------------------------------------------

        private static int Hash_1(int sayi, int tabloBoyutu)// say�n�n tablo boyutuna g�re modu
        {
            int sonuc;
            sonuc = sayi % tabloBoyutu;
            return sonuc;
        }

        private static int Hash_2(int sayi, int tabloBoyutu)// say�/tablo boyutu
        {
            int sonuc;
            sonuc = sayi / tabloBoyutu;
            if (sonuc == 0)
            {
                sonuc = 1;
            }
            return sonuc;
        }

        public static void TabloYenile(int[] tablo)//tablonun b�t�n elemanlar�na -1 yaz.
        {
            for (int i = 0; i < tablo.Length; i++)
            {
                tablo[i] = -1;
            }
        }

        public static void menuGoster()
        {
            Console.WriteLine("1.Ekle\n2.Ara\n\n");
        }

        // AMELE FONKS�YONLAR SON -----------------------------------------------------------------
    }

}
