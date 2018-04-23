using SD.localhost;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SD
{
    public partial class Form2 : Form
    {
        private string usuario;
        private string clave = "SDLarrySD2016";

        public Form2(string usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bienvenida.Text = "¡Bienvenido/a " + usuario + "!";
        }

        public String getIP()
        {
            IPHostEntry host;
            String local = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    local = ip.ToString();
                }
            }
            return local;
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

        public void log(String accion, String valor, String ipSonda)
        {
            System.IO.StreamWriter log = new System.IO.StreamWriter("log.txt", true);
            log.WriteLine("[Usuario: " + usuario + "\t| " 
                + "IP: " + getIP() + "\t| "
                + "Fecha: " + DateTime.Now.ToString() + "\t| "
                + accion + ": " + valor + "\t| "
                + "Estacion: " + ipSonda + "\t]");
            log.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (esSondaCorrecta(textBox1.Text))
                {
                    comboBox1.Items.Add(textBox1.Text);
                    comboBox3.Items.Add(textBox1.Text);
                    listBox1.Items.Add(textBox1.Text);
                }
                else
                {
                    MessageBox.Show("¡La sonda introducida no es correcta!");
                }
            }
        }

        private bool esSondaCorrecta(string sonda)
        {
            return true; //futura ampliacion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i] == listBox1.SelectedItem)
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    comboBox1.Items.RemoveAt(i);
                    comboBox3.Items.RemoveAt(i);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("¡Introduzca primero la sonda a consultar!");
            }
            else if(comboBox2.Text == "")
            {
                MessageBox.Show("¡Introduzca el parámetro de la sonda a consultar!");
            }
            else
            {
                try
                {
                    string ipSonda = comboBox1.SelectedItem.ToString();
                    ObjetoRemoto objetoRemoto = new ObjetoRemoto();
                    objetoRemoto.Url = "http://" + ipSonda + "/SD/services/ObjetoRemoto?wsdl";
                    string accion = "Obtener";
                    if(comboBox2.Text == "Volumen")
                    { 
                        label1.Text = "El volumen consultado es: " +
                        Decrypt(objetoRemoto.getVolumen(
                            Encrypt(getIP(),clave),
                            Encrypt(usuario, clave)),
                        clave);
                        log(accion, comboBox2.Text, ipSonda);
                    }
                    else if(comboBox2.Text == "Fecha")
                    {
                        label1.Text = "La fecha consultada es: " +
                        Decrypt(objetoRemoto.getFecha(
                            Encrypt(getIP(), clave),
                            Encrypt(usuario, clave)),
                        clave);
                        log(accion, comboBox2.Text, ipSonda);
                    }
                    else if(comboBox2.Text == "Ultima Fecha")
                    {
                        label1.Text = "La ultima fecha consultada es: " +
                        Decrypt(objetoRemoto.getUltimaFecha(
                            Encrypt(getIP(), clave),
                            Encrypt(usuario, clave)),
                        clave);
                        log(accion, comboBox2.Text, ipSonda);
                    }
                    else if(comboBox2.Text == "Luz")
                    {
                        label1.Text = "La luz consultada es: " +
                        Decrypt(objetoRemoto.getLuz(
                            Encrypt(getIP(), clave),
                            Encrypt(usuario, clave)),
                        clave);
                        log(accion, comboBox2.Text, ipSonda);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Sonda no válida, vuelve a seleccionar otra!");
                }

                try
                {
                    string valor = comboBox2.SelectedItem.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Parámetro no válido, vuelve a seleccionar otro!");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "")
            {
                MessageBox.Show("¡Introduzca primero la sonda a modificar!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("¡Introduzca el valor de la luz a modificar!");
            }
            else
            {
                try
                {
                    string ipSonda = comboBox3.SelectedItem.ToString();
                    ObjetoRemoto objetoRemoto = new ObjetoRemoto();
                    objetoRemoto.Url = "http://" + ipSonda + "/SD/services/ObjetoRemoto?wsdl";
                    string accion = "Modificar";

                    objetoRemoto.setLuz(Encrypt(textBox2.Text, clave),
                        Encrypt(getIP(), clave), 
                        Encrypt(usuario, clave));
                    label2.Text = "La luz se ha modificado con: " + textBox2.Text;

                    log(accion, "Luz", ipSonda);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Sonda no válida, vuelve a seleccionar otra!");
                }

                string valorLed = textBox2.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
        }
    }
}
