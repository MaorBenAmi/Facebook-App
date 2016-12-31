using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    class CommentWrapper:Fetchable
    {
        private Comment m_CommentFB;

        public CommentWrapper(Comment i_Comment)
        {
            CommentFB = i_Comment;
        }

        public Comment CommentFB
        {
            get { return m_CommentFB; }
            set { m_CommentFB = value; }
        }
        public override string ToString()
        {
            return this.CommentFB.Message;
        }
            
    }
}
