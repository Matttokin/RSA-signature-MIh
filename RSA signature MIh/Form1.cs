using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_signature_MIh
{
    public partial class Form1 : Form
    {
        private int createMaxPrimValue(int end)
        {
            for (int i = end; i >= 2; i--)
            {
                bool b = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0 & i % 1 == 0)
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    return i;
                }
            }
            return 1;
        }
        public Form1()
        {
            InitializeComponent();
        }     
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int q = Convert.ToInt32(textBox1.Text.ToString());
                int p = Convert.ToInt32(textBox2.Text.ToString());
                int n = q * p;
                int func = (p - 1) * (q - 1);
                Console.WriteLine(func);
                int eZhach = createMaxPrimValue(func - 1);
                BigInteger d = modInverse(eZhach, func);
                textBox11.Text = eZhach.ToString();
                textBox12.Text = d.ToString();
                textBox13.Text = n.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox6.Text = "";
                int d = Convert.ToInt32(textBox3.Text.ToString());
                int n = Convert.ToInt32(textBox4.Text.ToString());
                string m = textBox5.Text.ToString();
                for (int i = 0; i < m.Length; i++)
                {
                    textBox6.Text += BigInteger.ModPow(Convert.ToInt32(m[i]), d, n).ToString() + "#";
                }
                textBox6.Text = textBox6.Text.Remove(textBox6.Text.Length - 1, 1);
            } catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textBox11.Text = "";
                int eZhach = Convert.ToInt32(textBox7.Text.ToString());
                int n = Convert.ToInt32(textBox8.Text.ToString());
                string m = textBox9.Text.ToString();
                string[] arrM = textBox10.Text.Split('#');
                string tmpM = "";
                for (int i = 0; i < arrM.Length; i++)
                {
                    tmpM += ((char)BigInteger.ModPow(Convert.ToInt32(arrM[i]), eZhach, n)).ToString();
                }
                if(m == tmpM)
                {
                    textBox14.Text = "Сообщение прошло проверку";
                } else
                {
                    textBox14.Text = "Сообщение не прошло проверку";
                }
            } catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
