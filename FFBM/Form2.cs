using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;

namespace FFBM
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        public string MyProperty { get; set; }
        public List<string> ListImages { get; set; }
        private int selected = 0;
        private int begin = 0;
        private int end = 0;

        private void Form2_Load_1(object sender, EventArgs e)
        {
            string path = this.MyProperty;
            ListImages.ForEach(Console.WriteLine);
            Console.WriteLine("Count: {0}", ListImages.Count);
            selected = 0;
            begin = 0;
            end = ListImages.Count;
            showImages(ListImages[selected]);
            prev.Enabled = true;
            next.Enabled = true;
            cancel.Enabled = true;
            save.Enabled = true;
        }

        private void showImages(string path)
        {
            Image imgtemp = Image.FromFile(path);
            pictureBox1.Width = imgtemp.Width ;
            pictureBox1.Height = imgtemp.Height ;
            pictureBox1.Image = imgtemp;
        }

        private void prev_Click(object sender, EventArgs e)
        {
            if (selected == 0)
            {
                selected = ListImages.Count - 1;
                showImages(ListImages[selected]);
            }
            else
            {
                selected = selected - 1; showImages(ListImages[selected]);
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (selected == ListImages.Count - 1)
            {
                selected = 0;
                showImages(ListImages[selected]);
            }
            else
            {
                selected = selected + 1; showImages(ListImages[selected]);
            }
        }

        private async Task sendPhotosAsync()
        {
            var values = new Dictionary<string, string>
            {

                { "photo", ListImages[0] },
                { "photo",ListImages[1] },
                { "photo", ListImages[2] },
                { "photo", ListImages[3] },
                { "photo",ListImages[4] },
                { "photo", ListImages[5] }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost:8080/save_photos", content);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private void save_Click(object sender, EventArgs e)
        {
            //await sendPhotosAsync();

        }
    }
}
