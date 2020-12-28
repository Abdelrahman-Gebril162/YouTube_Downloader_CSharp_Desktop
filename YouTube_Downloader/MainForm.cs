using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using MediaToolkit;
namespace YouTube_Downloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            success.Visible =false;
            failed.Visible=false;
        }
        Boolean formate = true;
        

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fdb =new FolderBrowserDialog() {Description = "Select Folder Please"})
            {
                if (fdb.ShowDialog()==DialogResult.OK)
                {
                    MessageBox.Show("Folder Selected", "Selection", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    progressBar1.Value = 10;
                    var yt = YouTube.Default;
                    progressBar1.Value += 10;
                    YouTubeVideo video;
                    try
                    {
                        video = await yt.GetVideoAsync(txtAddress.Text);
                        progressBar1.Value += 10;
                        File.WriteAllBytes(fdb.SelectedPath + @"\" + video.FullName, await video.GetBytesAsync());
                        progressBar1.Value += 10;
                        var inputFile = new MediaToolkit.Model.MediaFile(filename: fdb.SelectedPath + @"\" + video.FullName);
                        var outputfile = new MediaToolkit.Model.MediaFile(filename: $"{ fdb.SelectedPath + @"\" + video.FullName}.mp3");
                        progressBar1.Value += 20;
                        using (var engine = new Engine())
                        {
                            engine.GetMetadata(inputFile);
                            engine.Convert(inputFile, outputfile);
                        }
                        progressBar1.Value += 20;
                        if (formate)
                        {
                            File.Delete(fdb.SelectedPath + @"\" + video.FullName);
                        }

                        else
                        {
                            File.Delete($"{fdb.SelectedPath + @"\" + video.FullName}.mp3");
                        }
                        progressBar1.Value += 20;
                        success.Visible=true;
                        Thread.Sleep(1000);
                        progressBar1.Value = 0;
                        success.Visible=false;
                    }
                    catch (Exception ex)
                    {
                        txtAddress.Text = "";
                        MessageBox.Show("not valid url");
                        failed.Visible=true;
                        Thread.Sleep(1000);
                        progressBar1.Value = 0;
                        failed.Visible=false;
                    }
                    
                    


                }
                else
                {
                   failed.Visible=true;
                   Thread.Sleep(1000);
                   progressBar1.Value = 0;
                   failed.Visible=false;
                }
            }
        }

        private void rbtnmp3_TextChanged(object sender, EventArgs e)
        {
            formate = true;
        }

        private void rbtnmp4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtnmp4_TextChanged(object sender, EventArgs e)
        {
            formate = false;
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAddress.Text == "Type your Address")
            {
                txtAddress.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want To Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
               Environment.Exit(1);
            }
            
        }
    }
}
