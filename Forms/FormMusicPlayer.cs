using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public partial class FormMusicPlayer : Form
    {
        private Dictionary<string, string> m_MyPlaylist;
        private string m_PlaylistName;

        public string PlaylistName
        {
            get { return m_PlaylistName; }
            set { m_PlaylistName = value; }
        }

        public FormMusicPlayer(string i_PlaylistName,Dictionary<string, string> i_MyPlaylist)
        {
            InitializeComponent();
            MyPlaylist = new Dictionary<string, string>();
            MyPlaylist = i_MyPlaylist;
            PlaylistName= i_PlaylistName;
            uploadPlaylist();
        }

        private void uploadPlaylist()
        {
            this.labelPlaylistName.Text = PlaylistName;
            foreach (string des in MyPlaylist.Keys)
            {
                this.listBoxPlaylistSongs.Items.Add(des);
            }
            }
        public Dictionary<string, string> MyPlaylist
        {
            get { return m_MyPlaylist; }
            set { m_MyPlaylist = value; }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            playVideo();
        }

        private void playVideo()
        {
            if(listBoxPlaylistSongs.SelectedIndex == -1)
            {
                listBoxPlaylistSongs.SelectedIndex = 0;
            }
            string des = (listBoxPlaylistSongs.SelectedItem as string);
            string url;
            MyPlaylist.TryGetValue(des, out url);
            webBrowserMediaPlayer.Navigate(url);
        }

        private void listBoxPlaylistSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            playVideo();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            webBrowserMediaPlayer.Stop();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (listBoxPlaylistSongs.SelectedIndex < listBoxPlaylistSongs.Items.Count)
            {
                listBoxPlaylistSongs.SelectedIndex++;
                playVideo();
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (listBoxPlaylistSongs.SelectedIndex > 0)
            {
                listBoxPlaylistSongs.SelectedIndex--;
                playVideo();
            }
        }

    }
}
