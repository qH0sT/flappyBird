using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flappyBird
{
    public partial class Form1 : Form     // GÖZLERİM SANSÜR PERDESİİİİİ. https://www.youtube.com/watch?v=JhquGf1pi_Y
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.ImageLocation =Environment.CurrentDirectory + "\\Res\\bird.gif";
        }
        bool yukari = false;
        List<PictureBox> pictirBaks = new List<PictureBox>();
        private async void assagiIndir()
        {
            await Task.Run(async () => {
                while (yukari == false)
                {
                    Invoke((MethodInvoker)delegate {
                        pictureBox1.Top += 5;
                    });
                    await Task.Delay(1);
                }
            });
        }
        private async void KolonHareket()
        {
            await Task.Run(async () => {
                while (true)
                {
                    Invoke((MethodInvoker)delegate {
                        foreach (PictureBox pcbx in pictirBaks)
                        {
                            if (pictirBaks.Count % 2 == 0)
                            {
                                pcbx.Left -= 40;
                                pcbx.Left -= 40;
                            }
                        }
                        foreach (PictureBox pcbx in point)
                        {
                         
                                pcbx.Left -= 40;
                                pcbx.Left -= 40;
                           
                        }
                    });

                    if (pictirBaks.Any(x => pictureBox1.Bounds.IntersectsWith(x.Bounds)))
                    {
                        pictureBox1.BackColor = Color.Green;
                        label2.Text = "Yandınız.";
                        break;
                    }
                     if (point.Any(d => pictureBox1.Bounds.IntersectsWith(d.Bounds)))
                    {
                        puan += 10;
                        if (puan % 3 == 0)
                        {
                            Invoke((MethodInvoker)delegate {
                                foreach (var b in pictirBaks)
                                {
                                    Controls.Remove(b);
                                }
                                foreach (var b in point)
                                {
                                    Controls.Remove(b);
                                }
                                point.Clear();
                                pictirBaks.Clear();
                              
                            label2.Text = "Seviye atlandı";
                            kolonlariDiz();
                            });
                        }
                      
                        label1.Text = puan.ToString();
                    }
                   
                    await Task.Delay(200);
                    label2.Text = "...";
                    
                }
            });
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                yukari = true;
                pictureBox1.Top -= 65;
            }
        }
        private void kolonlariDiz()
        {
            PictureBox pcbX = null;
            PictureBox puantaj = null;
            for (int i = 0; i < 6; i++)
            {
                pcbX = new PictureBox();
                pcbX.BackColor = Color.Green;

                if (i % 2 == 0)
                {
                    pcbX.Top = 0;
                    pcbX.Left = Size.Width;
                    switch (tutucu)
                    {
                        case 0:
                            pcbX.Size = new Size(68, 153 + 400);
                            break;
                        case 1:
                            pcbX.Top = -100;
                            pcbX.Size = new Size(68, 153);
                            break;
                        case 2:
                            pcbX.Size = new Size(68, 153 + 100);
                            break;
                    }

                }
                else
                {
                    pcbX.Left = Size.Width;
                    puantaj = new PictureBox();
                    puantaj.Top = 0;
                    puantaj.BackColor = Color.Red;
                    puantaj.Size = new Size(40, 700);
                    puantaj.Visible = false;
                    puantaj.Left = pcbX.Left + 100;
                    point.Add(puantaj);
                    Controls.Add(puantaj);
                    switch (tutucu)
                    {
                        case 0:
                            pcbX.Top = Size.Height - 100;
                            pcbX.Size = new Size(68, 153 + 500);
                            break;
                        case 1:
                            pcbX.Top = Size.Height - 600;
                            pcbX.Size = new Size(68, 153 + 500);
                            break;
                        case 2:
                            pcbX.Top = Size.Height - 400;
                            pcbX.Size = new Size(68, 153 + 400);
                            break;
                    }
                    if ((i + 1) % 2 == 0) { tutucu = rnd.Next(0, 2); }

                }
                Controls.Add(pcbX);
                pictirBaks.Add(pcbX);

                foreach (Control cntrl in Controls)
                {
                    if (cntrl is PictureBox)
                    {
                        if (cntrl.Name != "pictureBox1")
                        {
                            if (pictirBaks.Count % 2 == 0)
                            {
                                cntrl.Left += 780;
                            }
                        }
                    }
                }
            }
        }
        int tutucu = 1;
        int puan = 0;
        Random rnd = new Random();
        List<PictureBox> point = new List<PictureBox>();
        private void Form1_Load(object sender, EventArgs e)
        {
            kolonlariDiz();
            assagiIndir();
            KolonHareket();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                yukari = false;
                assagiIndir();
            }
        }
    }
}
