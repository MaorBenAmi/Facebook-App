using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.Globalization;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public partial class FormFaceBook : Form
    {
        private LogicManagment m_LogicManagment;


        private bool m_IsLoginSuccessed;
        private bool m_ShowAll;
        private bool m_IgnoreSelectIndexChangedEvent;
        public FormFaceBook()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 200;
            FacebookWrapper.FacebookService.s_FbApiVersion = 2.8f;
            LogicManagment = new LogicManagment();
            m_IsLoginSuccessed = false;
            m_ShowAll = false;
            m_IgnoreSelectIndexChangedEvent = true;
            disableMainTab();
        }

        internal LogicManagment LogicManagment
        {
            get { return m_LogicManagment; }
            set { m_LogicManagment = value; }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            m_IsLoginSuccessed = LogicManagment.userLogin();
            if (m_IsLoginSuccessed == true)
            {
                userInfoUpload();
            }
        }


        private void userInfoUpload()
        {
            pictureBoxProfilePicture.LoadAsync(LogicManagment.LoggedInUser.UserFB.PictureNormalURL);
            labelFullName.Text = LogicManagment.LoggedInUser.UserFB.Name;
            labelBirthday.Text = LogicManagment.LoggedInUser.UserFB.Birthday;
            if (LogicManagment.LoggedInUser.UserFB.Location.Name != null)
            {
                labelLocation.Text = LogicManagment.LoggedInUser.UserFB.Location.Name;
            }
            if (LogicManagment.LoggedInUser.UserFB.Educations.Count() > 0)
            {
                LogicManagment.fetchInformationFromUser(typeof(Education));
                listBoxEduction.DataSource = LogicManagment.FetchesObjects;
                listBoxEduction.SelectedIndex = -1;
            }

            LogicManagment.fetchInformationFromUser(typeof(Post));
            listBoxPreviousPosts.DataSource = LogicManagment.FetchesObjects;


            LogicManagment.fetchInformationFromUser(typeof(User));
            listBoxFetchFriends.DataSource = LogicManagment.FetchesObjects;


            LogicManagment.fetchInformationFromUser(typeof(Event));
            listBoxFetchEvents.DataSource = LogicManagment.FetchesObjects;

            LogicManagment.fetchSongs();
            fillFriendsVideosDataGridView();
            activeMainTab();
            m_IgnoreSelectIndexChangedEvent = false;
        }


        private void buttonApply_Click(object sender, EventArgs e)
        {
            disableActivitesTab();
            m_ShowAll = false;
            listBoxPlacesSearchingResult.DataSource = null;
            string dayOfTheWeek=string.Empty;
            string keyword = string.Empty;
            if (checkBoxDayOfTheWeek.Checked)
            {
                dayOfTheWeek = comboBoxDayOfTheWeek.Text;
            }
            if (checkBoxKeyword.Checked)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                keyword = textInfo.ToTitleCase(textBoxKeyword.Text);
            }
            if (!checkBoxDayOfTheWeek.Checked && !checkBoxKeyword.Checked)
            {
                m_ShowAll = true;
            }
            
            LogicManagment.filterOrShowAll(dayOfTheWeek, keyword,m_ShowAll);
            listBoxPlacesSearchingResult.DataSource = LogicManagment.FilterCheckins.ListOfFilteredCheckins;
            activeActivitiesTab();
        }

        private void activeActivitiesTab()
        {
            listBoxPlacesSearchingResult.SelectedIndexChanged += listBoxPlacesSearchingResult_SelectedIndexChanged;
            listBoxPlacesSearchingResult.SelectedIndex = -1;
        }

        private void disableActivitesTab()
        {
            listBoxPlacesSearchingResult.SelectedIndexChanged -= listBoxPlacesSearchingResult_SelectedIndexChanged;
            listBoxFriendsWhoCheckedIn.DataSource = null;
            listBoxCommentsOnCheckin.DataSource = null;
            pictureBoxChoosenCheckin.Image=null;
            labelCheckinName.Text = string.Empty;
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            Status postedStatus = m_LogicManagment.LoggedInUser.UserFB.PostStatus(textBoxStatus.Text);
            MessageBox.Show("Status Posted!");
        }

        private void listBoxPreviousPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFriendsComments.DataSource = null;
            if (!m_IgnoreSelectIndexChangedEvent)
            {
                displayPreviousPosts();
            }
        }

        private void disableMainTab()
        {

            tabControlMain.Enabled = false;
            listBoxPreviousPosts.SelectedIndexChanged -= listBoxPreviousPosts_SelectedIndexChanged;
            listBoxFetchFriends.SelectedIndexChanged -= listBoxFetchFriends_SelectedIndexChanged;
            listBoxFetchEvents.SelectedIndexChanged -= listBoxFetchEvents_SelectedIndexChanged;
        }


        private void activeMainTab()
        {
            tabControlMain.Enabled = true;
            listBoxPreviousPosts.SelectedIndexChanged += listBoxPreviousPosts_SelectedIndexChanged;
            listBoxFetchFriends.SelectedIndexChanged += listBoxFetchFriends_SelectedIndexChanged;
            listBoxFetchEvents.SelectedIndexChanged += listBoxFetchEvents_SelectedIndexChanged;
            listBoxPreviousPosts.SelectedIndex = -1;   
            listBoxFetchFriends.SelectedIndex=-1;
            listBoxFetchEvents.SelectedIndex = -1;
        
        
        }


        private void displayPreviousPosts()
        {
            if (listBoxPreviousPosts.SelectedItems.Count == 1)
            {
                PostWarpper selectedPost = listBoxPreviousPosts.SelectedItem as PostWarpper;
                LogicManagment.fetchInformationFromPost(selectedPost, typeof(Comment));
                listBoxFriendsComments.DataSource = LogicManagment.FetchesObjects;

            }
        }
        private void listBoxFriendsComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFriendsComments.SelectedItems.Count == 1 )
            {
                CommentWrapper selectedFriend = listBoxFriendsComments.SelectedItem as CommentWrapper;
                if (selectedFriend.CommentFB.From.PictureNormalURL != null)
                {
                    pictureBoxLoaded.LoadAsync(selectedFriend.CommentFB.From.PictureNormalURL);
                    labelFriendName.Text = selectedFriend.CommentFB.From.Name;
                }
            }
        }

        private void listBoxPlacesSearchingResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPlacesSearchingResult.SelectedItems.Count == 1)
            {
                CheckinWrapper selectedCheckin = listBoxPlacesSearchingResult.SelectedItem as CheckinWrapper;
                if (selectedCheckin.CheckinFB.Place.PictureNormalURL != null)
                {
                    pictureBoxChoosenCheckin.LoadAsync(selectedCheckin.CheckinFB.Place.PictureNormalURL);
                    labelCheckinName.Text = selectedCheckin.CheckinFB.Name;
                    labelCheckinLocation.Text = selectedCheckin.CheckinFB.Place.Name;
                }
                listBoxFriendsWhoCheckedIn.DataSource = null;
                LogicManagment.fetchInformationFromCheckin(selectedCheckin,typeof(User));
                listBoxFriendsWhoCheckedIn.DataSource = LogicManagment.FetchesObjects;
                listBoxFriendsWhoCheckedIn.SelectedIndex = -1;

                listBoxCommentsOnCheckin.DataSource = null;
                LogicManagment.fetchInformationFromCheckin(selectedCheckin, typeof(Comment));
                listBoxCommentsOnCheckin.DataSource = LogicManagment.FetchesObjects;
                listBoxCommentsOnCheckin.SelectedIndex = -1;
                
            }
            
        }

        private void listBoxFetchFriends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFetchFriends.SelectedItems.Count == 1)
            {
                UserWrapper selectedFriend = listBoxFetchFriends.SelectedItem as UserWrapper;
                if (selectedFriend.UserFB.PictureNormalURL != null)
                {
                    pictureBoxLoaded.LoadAsync(selectedFriend.UserFB.PictureNormalURL);
                    labelFriendName.Text = selectedFriend.UserFB.Name;
                }
            }
        }

        private void listBoxFetchEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFetchEvents.SelectedItems.Count == 1)
            {
                EventWrapper selectedEvent = listBoxFetchEvents.SelectedItem as EventWrapper;
                if (selectedEvent.EventFB.PictureNormalURL != null)
                {
                    pictureBoxLoaded.LoadAsync(selectedEvent.EventFB.PictureNormalURL);
                }
            }
        }


        private void fillFriendsVideosDataGridView()
        {
            int index;
            DataGridViewRow row;
                    foreach (FriendMedia friend in LogicManagment.FriendsSongsList)
                    {
                        foreach(PostWarpper song in friend.ListMediaUrl)
                        {
                            index = dataGridView1.Rows.Add();
                            row = dataGridView1.Rows[index];
                            row.Cells["Friend"].Value = friend.Friend;
                            row.Cells["SongDescription"].Value = song.PostFB.Description;
                            row.Cells["VideoUrl"].Value = song.PostFB.Link;
                        }

                    }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if ((sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                webBrowser1.Navigate(dataGridView1.Rows[e.RowIndex].Cells["VideoUrl"].Value.ToString());
                webBrowser1.Visible = false;
            }
        }

        private void buttonPostSong_Click(object sender, EventArgs e)
        {
            LogicManagment.LoggedInUser.UserFB.PostLink(webBrowser1.Url.ToString(),textBoxPostSong.Text);
            MessageBox.Show("Share Successed !");
        }

        private void buttonCreatePlaylist_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            string songDescription = string.Empty;
            string songUrl = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            { 
            if(Convert.ToBoolean(row.Cells[0].Value) == true)
            {
                songDescription=row.Cells[3].Value.ToString();
                songUrl=row.Cells[4].Value.ToString();
                myDic.Add(songDescription,songUrl);

            }
            }
            FormMusicPlayer musicPlayer = new FormMusicPlayer(textBoxPlaylistName.Text, myDic);
            musicPlayer.ShowDialog();
        }
    }
}

