using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOyun
{
    class Oyun
    {
        public delegate void CizEventHandler();
        public delegate void OyunBittiEventHandler();
        public static event CizEventHandler OnCiz;
        public static event OyunBittiEventHandler OnOyunBitti;

        public int OyunGecikmesi { get; set; }
        public Sekil[] SekilOrnekleri { get; private set; }
        public Tahta Tahta { get; private set; }
        public Sekil AktifSekil { get; private set; }
        public int Skor { get; private set; }
        public bool OyunBittiMi { get; set; }
        public bool OyunBasladiMi { get; set; }

        private Random Rndm = new Random();
        private readonly Stopwatch OyunZamani = new Stopwatch();
        /// <summary>
        /// Oyun için gerekli özellikleri oluşturur.
        /// </summary>
        public Oyun()
        {
            Tahta = new Tahta(10, 20);
            SekilleriOlustur();
            OyunGecikmesi = 250;
            AktifSekil = new Sekil(SekilOrnekleri[Rndm.Next(0, 7)], new Point(4, 0), Tahta);
        }
        /// <summary>
        /// Tetris şekillerini oluşturur.
        /// </summary>
        private void SekilleriOlustur()
        {
            SekilOrnekleri = new Sekil[7];
            SekilOrnekleri[0] = new Sekil(new Nokta(0.5, 1.5), new Nokta[4] { new Nokta(0, 2), new Nokta(0, 1), new Nokta(0, 0), new Nokta(0, -1) }, Tetromino.I); 
            SekilOrnekleri[1] = new Sekil(new Nokta(1.5, 0.5), new Nokta[4] { new Nokta(-1, 0), new Nokta(0, 1), new Nokta(0, 0), new Nokta(1, 0) }, Tetromino.T); 
            SekilOrnekleri[2] = new Sekil(new Nokta(1, 1), new Nokta[4] { new Nokta(0.5, 0.5), new Nokta(0.5, -0.5), new Nokta(-0.5, 0.5), new Nokta(-0.5, -0.5) }, Tetromino.O);
            SekilOrnekleri[3] = new Sekil(new Nokta(1, 2), new Nokta[4] { new Nokta(-0.5, -1.5), new Nokta(-0.5, -0.5), new Nokta(-0.5, 0.5), new Nokta(0.5, 0.5) }, Tetromino.L);
            SekilOrnekleri[4] = new Sekil(new Nokta(1, 2), new Nokta[4] { new Nokta(0.5, -1.5), new Nokta(0.5, -0.5), new Nokta(-0.5, 0.5), new Nokta(0.5, 0.5) }, Tetromino.J); 
            SekilOrnekleri[5] = new Sekil(new Nokta(1.5, 0.5), new Nokta[4] { new Nokta(1, 0), new Nokta(0, 0), new Nokta(0, 1), new Nokta(-1, 1) }, Tetromino.S); 
            SekilOrnekleri[6] = new Sekil(new Nokta(1.5, 0.5), new Nokta[4] { new Nokta(-1, 0), new Nokta(0, 0), new Nokta(0, 1), new Nokta(1, 1) }, Tetromino.Z); 
        }
        /// <summary>
        /// Oyunu başlatır, oyun bitene kadar döngü devam eder. OyunForm ile eventler ile haberleşir. Her bir iterationda bütün şekiller ve tahta yeniden çizilir.
        /// </summary>
        public void Baslat()
        {
            OyunBasladiMi = true;
            OyunZamani.Restart();
            while (!OyunBittiMi)
            {
                if (!AktifSekil.HareketEttir(new Point(0, 1)))
                {
                    Tahta.SekliDurdur(AktifSekil); 
                    if (Tahta.SatilariSil() > 0)
                    {
                        Skor += 100;
                    }
                    if (Tahta.OyunBittiMi())
                    {
                        Skor += (int)TimeSpan.FromMilliseconds(OyunZamani.ElapsedMilliseconds).TotalSeconds;
                        OyunBittiMi = true;
                        OnOyunBitti();
                        break;
                    }
                    else
                    {
                        AktifSekil = new Sekil(SekilOrnekleri[Rndm.Next(0, 7)], new Point(4, 0), Tahta);

                    }
                }
                OnCiz();
                Thread.Sleep(OyunGecikmesi);
            }
        }

    }
}
