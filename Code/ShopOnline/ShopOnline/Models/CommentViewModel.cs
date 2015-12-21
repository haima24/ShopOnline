using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentDetail { get; set; }
        public System.DateTime CommentDate { get; set; }
        public int CommentUserId { get; set; }
        public int? ProductId { get; set; }
        public int? ParentCommentId { get; set; }

        public string CommentUserName { get; set; }
        public List<CommentViewModel> ChildsComment { get; set; }
        public CommentViewModel ParentComment { get; set; }
    }
}