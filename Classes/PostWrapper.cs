using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public class PostWarpper : Fetchable
    {
        private Post m_PostFB;

        public PostWarpper(Post i_Post)
        {
            PostFB = i_Post;
        }

        public Post PostFB
        {
            get { return m_PostFB; }
            set { m_PostFB = value; }
        }
        public override string ToString()
        {
            if (this.PostFB.Message != null)
            {
                return this.PostFB.Message;
            }
            else if (this.PostFB.Caption != null)
            {
                return this.PostFB.Caption;
            }
            else if(this.PostFB.Description != null)
            {
                return this.PostFB.Description;
            }
            else
            {
                return string.Format("{0}", this.PostFB.Type);
            }
        }
    }
}
