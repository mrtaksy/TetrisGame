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
        /// <summary>
        /// tahtagenisligi ve tahtayuksekligi parametrelerini alarak nesne özelliklerine set eder ve o boyutlarda matriks oluşturur.
        /// </summary>
        /// <param name="tahtaGenisligi"></param>
        /// <param name="tahtaYuksekligi"></param>
        public Tahta(int tahtaGenisligi, int tahtaYuksekligi)
        {
            TAHTA_GENISLIGI = tahtaGenisligi;
            TAHTA_YUKSEKLİGİ = tahtaYuksekligi;
            Bloklar = new int[tahtaGenisligi, tahtaYuksekligi];
        }
        /// <summary>
        /// Gönderilen şekli durdurur, mevcut kordinatlara Tertomino enum değerlerini set eder. (0 dan büyük olarak)
        /// </summary>
        /// <param name="t"></param>
        internal void SekliDurdur(Sekil t)
        {
            Point[] kordinatlar = t.GetBloklar();
            for (int i = 0; i < 4; i++)
            {
                Bloklar[kordinatlar[i].X, kordinatlar[i].Y] = (int)t.Tertomino;
            }
        }
        /// <summary>
        /// Dolmuş satırları siler
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Gönderilen indexdeki satırları siler
        /// </summary>
        /// <param name="index"></param>
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
        /// <summary>
        /// Oyunun bitip bitmediğini kontrol eder.
        /// </summary>
        /// <returns></returns>
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
