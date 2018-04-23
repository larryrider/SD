using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        public String Encrypt(String plainText, String key)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }

        public String Decrypt(String encryptedText, String key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clave = "SDLarrySD2016";
            string cadena = Encrypt(textBox1.Text + "#" + textBox2.Text, clave);

            //larry#larry  -->  iAKWDtuAbGPMY9awOtX1UQ==
            bool encontrado = false;

            string linea;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("login.txt");
                while (((linea = file.ReadLine()) != null) && !encontrado)
                {
                    if (linea.Equals(cadena))
                    {
                        encontrado = true;
                        MessageBox.Show("¡Login Correcto!");

                        String[] div = Decrypt(cadena,clave).Split('#');
                        string usuario = div[0];

                        Form2 form2 = new Form2(usuario);
                        form2.Show();
                        this.Hide();

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("¡Login Incorrecto!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("¡No se ha podido conectar con la base de datos de usuarios!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
