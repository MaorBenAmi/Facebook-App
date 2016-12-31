using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using System.Windows.Forms;
namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public class Fetcher
    {
        private List<Fetchable> m_FetchesObjects;
        private FriendMedia m_FriendMedias;
        private List<FriendMedia> m_FriendsMediasList;


        public Fetcher()
        {
            FetchesObjects = new List<Fetchable>();
        }

        public List<FriendMedia> FriendsMediasList
        {
            get { return m_FriendsMediasList; }
            set { m_FriendsMediasList = value; }
        }

        public FriendMedia FriendMedias
        {
            get { return m_FriendMedias; }
            set { m_FriendMedias = value; }
        }

        public List<Fetchable> FetchesObjects
        {
            get { return m_FetchesObjects; }
            set { m_FetchesObjects = value; }
        }

        public List<Fetchable> FetchInformation(Fetchable i_FetchingObject, Type i_TypeOfFetch)
        {
           FetchesObjects.Clear();
           UserWrapper newUserToAdd;
           PostWarpper newPostTAdd;
           EventWrapper newEventToAdd;
           EducationWrapper newEducationToAdd;
           CommentWrapper newCommentToAdd;

         if (i_FetchingObject is UserWrapper)
            {
                if (i_TypeOfFetch == typeof(User))
                {
                    foreach (User friend in (i_FetchingObject as UserWrapper).UserFB.Friends)
                    {
                        newUserToAdd = new UserWrapper(friend);
                        FetchesObjects.Add(newUserToAdd);
                    }
                }
                else if (i_TypeOfFetch == typeof(Event))
                {
                    foreach (Event myEvent in (i_FetchingObject as UserWrapper).UserFB.Events)
                    {
                        newEventToAdd = new EventWrapper(myEvent);
                        FetchesObjects.Add(newEventToAdd);
                    }
                }
                else if (i_TypeOfFetch == typeof(Post))
                {
                    foreach (Post post in (i_FetchingObject as UserWrapper).UserFB.Posts)
                    {
                        newPostTAdd = new PostWarpper(post);
                        FetchesObjects.Add(newPostTAdd);
                    }
                }
                else if (i_TypeOfFetch == typeof(Education))
                {
                    foreach (Education myEd in (i_FetchingObject as UserWrapper).UserFB.Educations)
                    {
                        newEducationToAdd = new EducationWrapper(myEd);
                        FetchesObjects.Add(newEducationToAdd);
                    }
                }
             }
         else if (i_FetchingObject is PostWarpper)
         {
             if (i_TypeOfFetch == typeof(Comment))
             {
                 foreach (Comment friendComment in (i_FetchingObject as PostWarpper).PostFB.Comments)
                 {
                     newCommentToAdd = new CommentWrapper(friendComment);
                     FetchesObjects.Add(newCommentToAdd);
                 }
             }
         }
          if (i_FetchingObject is CheckinWrapper)
         {
             if (i_TypeOfFetch == typeof(User))
             {
                 foreach (User checkinedFriends in (i_FetchingObject as CheckinWrapper).CheckinFB.WithUsers)
                 {
                     newUserToAdd = new UserWrapper(checkinedFriends);
                     FetchesObjects.Add(newUserToAdd);
                }
             }
             else if (i_TypeOfFetch == typeof(Comment))
             {
                 foreach (Comment friendsComment in (i_FetchingObject as CheckinWrapper).CheckinFB.Comments)
                 {
                     newCommentToAdd = new CommentWrapper(friendsComment);
                     FetchesObjects.Add(newCommentToAdd);
                 }
             }
         
         }
         return FetchesObjects;
        }

        public List<FriendMedia> FetchFriendsMedias(UserWrapper i_User)
        {
            UserWrapper newFriend;
            PostWarpper newSongPost;
            FriendsMediasList = new List<FriendMedia>();
            foreach (User friend in i_User.UserFB.Friends)
            {
                newFriend = new UserWrapper(friend);
                FriendMedias = new FriendMedia(newFriend);

                foreach (Post post in friend.Posts)
                {
                    if (post.Link != null && post.Link.Contains("youtube"))
                    {
                        newSongPost = new PostWarpper(post);
                        FriendMedias.ListMediaUrl.Add(newSongPost);
                    }
                }
                FriendsMediasList.Add(FriendMedias);
            }
            return FriendsMediasList;
        }
    }
}
