using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisOyun
{
    class Sekil
    {
        internal Nokta Merkez { get; set; }
        private Nokta[] Bloklar;  
        internal Tetromino Tertomino { get; set; }
        private Point Pozisyon;
        private Tahta OynTahtasi;
        public Sekil(Nokta Merkez, Nokta[] Bloklar, Tetromino tetromino)
        {
            this.Merkez = Merkez;
            this.Bloklar = Bloklar;
            this.Tertomino = tetromino;
        }
        public Sekil(Sekil secilenSekil, Point pozisyon, Tahta oynTahtasi)
        {
            this.Tertomino = secilenSekil.Tertomino;
            this.Merkez = new Nokta(secilenSekil.Merkez.X, secilenSekil.Merkez.Y);
            this.Bloklar = new Nokta[4];
            for (int i = 0; i < 4; i++)
            {
                this.Bloklar[i] = new Nokta();
                this.Bloklar[i].X = secilenSekil.Bloklar[i].X;
                this.Bloklar[i].Y = secilenSekil.Bloklar[i].Y;
            }
            this.Pozisyon = new Point(pozisyon.X, pozisyon.Y);
            this.OynTahtasi = oynTahtasi;
        }
        internal Point[] GetBloklar()
        {
            Point[] result = new Point[5];
            for (int i = 0; i < 4; i++)
            {
                result[i] = new Point((int)(Pozisyon.X + Merkez.X + Bloklar[i].X), (int)(Pozisyon.Y + Merkez.Y + Bloklar[i].Y));
            }
            return result;
        }
        private bool CarpismaKontrol(Point hareketVek)  
        {
            for (int i = 0; i < 4; i++)
            {
                Point v = new Point();
                v.X = (int)Math.Floor(Pozisyon.X + Merkez.X + Bloklar[i].X);
                v.Y = (int)Math.Floor(Pozisyon.Y + Merkez.Y + Bloklar[i].Y);
                v.X += hareketVek.X;
                v.Y += hareketVek.Y;
                if ((v.X < 0) || (v.X >= Tahta.TAHTA_GENISLIGI) || (v.Y < 0) || (v.Y >= Tahta.TAHTA_YUKSEKLİGİ))
                    return false;
                if (OynTahtasi.Bloklar[v.X, v.Y] > 0)
                    return false;
            }
            return true;
        }
        internal bool HareketEttir(Point hareketVek)
        {
            if (CarpismaKontrol(hareketVek))
            {
                this.Pozisyon.X += hareketVek.X;
                this.Pozisyon.Y += hareketVek.Y;
                return true;
            }
            else return false;
        }
        internal bool Cevir(double aci)
        {
            Nokta[] temp = Bloklar;
            Bloklar = new Nokta[4];
            for (int i = 0; i < 4; i++)
            {
                Bloklar[i] = new Nokta();
                Bloklar[i].X = temp[i].X * Math.Cos(aci) - temp[i].Y * Math.Sin(aci);
                Bloklar[i].Y = temp[i].X * Math.Sin(aci) + temp[i].Y * Math.Cos(aci);
            }
            if (!CarpismaKontrol(new Point(0, 0)))
            {
                Bloklar = temp;
                return false;
            }
            return true;
        }
    }
}
