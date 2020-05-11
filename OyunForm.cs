﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisOyun
{
    public partial class OyunForm : Form, IMessageFilter
    {
        public OyunForm()
        {
            InitializeComponent();
        }

        Oyun oyun;
        Thread oynThread;
        bool IlkBaslama = false;
        /// <summary>
        /// Gerekli eventleri subscripe eder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OyunForm_Load(object sender, EventArgs e)
        {
            oyun = new Oyun();
            Oyun.OnCiz += Oyun_OnCiz;
            Oyun.OnOyunBitti += Oyun_OnOyunBitti;
            Application.AddMessageFilter(this);
        }
        /// <summary>
        /// oyun bitince puanı gösterir.
        /// </summary>
        private void Oyun_OnOyunBitti()
        {
            MessageBox.Show($"Oyun bitti, puanınız: {oyun.Skor}");
            oyun.OyunBittiMi = false;

        }

        /// <summary>
        /// Form onpaint eventinde gerekli oyun nesnelerini çizer.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 300, 600));
            if (oyun.OyunBasladiMi)
            {
                /// hızlı çizim için ayarlar
                e.Graphics.CompositingMode = CompositingMode.SourceCopy;
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;


                for (int i = 0; i < Tahta.TAHTA_GENISLIGI; i++)
                {
                    for (int j = 0; j < Tahta.TAHTA_YUKSEKLİGİ; j++)
                    {
                        if (oyun.Tahta.Bloklar[i, j] > 0)
                        {
                            e.Graphics.DrawImage(Properties.Resources.Parcacik, new Rectangle(30 * i, 30 * j, 30, 30));
                        }
                    }
                }

                Point[] kordinatlar = oyun.AktifSekil.GetBloklar();
                for (int i = 0; i < 4; i++)
                {
                    e.Graphics.DrawImage(Properties.Resources.Parcacik, new Rectangle(30 * kordinatlar[i].X, 30 * kordinatlar[i].Y, 30, 30));
                }
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Invalidate ile formu yeniden çizmeye zorlar.
        /// </summary>
        private void Oyun_OnCiz()
        {
            this.Invoke((Action)(() => { this.Invalidate(); }));

        }

        const int WM_KEYDOWN = 0x100;
        /// <summary>
        /// Basılan tuşları yakalar.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (msg.Msg)
            {
                case WM_KEYDOWN:
                    {
                        switch (keyData)
                        {
                            /// oyunu hızlandırır
                            case Keys.Down:
                                oyun.OyunGecikmesi = 40;
                                break;
                                /// şekli sola götürür
                            case Keys.Left:
                                if (oyun.AktifSekil.HareketEttir(new Point(-1, 0)))
                                    this.Invalidate();
                                break;
                                /// şekli sağa götürür
                            case Keys.Right:
                                if (oyun.AktifSekil.HareketEttir(new Point(1, 0)))
                                    this.Invalidate();
                                break;
                                /// şekli çevirir
                            case Keys.Up:
                                if (oyun.AktifSekil.Cevir(Math.PI / 2))
                                    this.Invalidate();
                                break; 
                        }
                    }
                    break; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        const int WM_KEYUP = 0x101;
        /// <summary>
        /// Tuş basma sona erince oyunun hızını normale döndürür.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYUP)
            {
                oyun.OyunGecikmesi = 250;
            }
            return false;
        }

        /// <summary>
        /// Oyunu başlatır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBaslat_Click(object sender, EventArgs e)
        {
            if (!IlkBaslama)
            {
                IlkBaslama = true;
                PicBaslat.Image = Properties.Resources.StartBtn_Off;
                PicYenidenBaslat.Image = Properties.Resources.ResBtn_On;
                oyun = new Oyun();
                oynThread = new Thread(() => oyun.Baslat()) { IsBackground = true };
                oynThread.Start();
            }
        }
        /// <summary>
        /// Oyunu resetler ve yeniden başlatır
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicYenidenBaslat_Click(object sender, EventArgs e)
        {
            if (IlkBaslama)
            {
                oyun.OyunBittiMi = true;
                if (oynThread.ThreadState == ThreadState.Running)
                    oynThread.Abort();
                oyun = new Oyun();
                oynThread = new Thread(() => oyun.Baslat()) { IsBackground = true };
                oynThread.Start();
            }
        }
        /// <summary>
        /// X butonuyla programı kapatmaya çalışınca oyun threadını sonra erdirir ve oyunu bitirip kapatır.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OyunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                oynThread.Abort();
                oyun.OyunBittiMi = true;
            }
            catch (Exception)
            {
            }
            Environment.Exit(0);
        }
    }
}