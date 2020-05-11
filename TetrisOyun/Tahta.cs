using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisOyun
{
    class Tahta
    {
        public static int TAHTA_GENISLIGI;
        public static int TAHTA_YUKSEKLİGİ;

        internal int[,] Bloklar { get; private set; } 
        public Tahta(int tahtaGenisligi, int tahtaYuksekligi)
        {
            TAHTA_GENISLIGI = tahtaGenisligi;
            TAHTA_YUKSEKLİGİ = tahtaYuksekligi;
            Bloklar = new int[tahtaGenisligi, tahtaYuksekligi];
        }
        internal void SekliDurdur(Sekil t)
        {
            Point[] kordinatlar = t.GetBloklar();
            for (int i = 0; i < 4; i++)
            {
                Bloklar[kordinatlar[i].X, kordinatlar[i].Y] = (int)t.Tertomino;
            }
        }
        internal int SatilariSil()
        {
            int result = 0;
            for (int i = 0; i < TAHTA_YUKSEKLİGİ; i++)
            {
                bool satirDoluMu = true;
                for (int j = 0; j < TAHTA_GENISLIGI; j++)
                {
                    if (Bloklar[j, i] == 0)
                    {
                        satirDoluMu = false;
                        break;
                    }
                }
                if (satirDoluMu)
                {
                    SatirSil(i--);
                    result += 1;
                }
            }
            return result;
        }
        private void SatirSil(int index)
        {
            for (int i = index; i >= 0; i--)
            {
                for (int j = 0; j < TAHTA_GENISLIGI; j++)
                {
                    if (i == 0)
                        Bloklar[j, i] = 0;
                    else
                        Bloklar[j, i] = Bloklar[j, i - 1];
                }
            }
        }
        internal bool OyunBittiMi()
        {
            bool result = false;
            for (int i = 0; i < TAHTA_GENISLIGI; i++)
            {
                if (Bloklar[i, 0] > 0)
                    result = true;
            }
            return result;
        }
    }
}
