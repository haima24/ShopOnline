using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShopOnline.App_Data;

namespace ShopOnline.Service
{
    public class CommentService : BaseService
    {
        
        public Comment CreateComment(string detail,int userId,int? productId,int? parentCommentId)
        {
            var comment = new Comment();
            comment.CommentDetail = detail;
            comment.CommentUserId = userId;
            comment.ProductId = productId;
            comment.CommentDate = DateTime.Now;
            comment.ParentCommentId = parentCommentId;
            Context.Comments.Add(comment);
            Context.SaveChanges();
            return comment;
        }
        public Comment GetComment(int commentId)
        {
            var commentDb = Context.Comments.Include(x=>x.User).FirstOrDefault(x => x.CommentId == commentId);
            return commentDb;
        }

        public int GetCommentCountByProduct(int productId)
        {
            var count = 0;
            var product = Context.Products.FirstOrDefault(x => x.ProductId == productId);
            if(product!=null)
            {
                count = product.Comments.Count;
            }
            return count;
        }

        public bool UpdateComment(string detail,int commentId,int userId)
        {
            var comment = Context.Comments.FirstOrDefault(x => x.CommentId == commentId && x.CommentUserId == userId);
            var result = false;
            if(comment!=null)
            {
                comment.CommentDetail = detail;
                result = Context.SaveChanges() > 0;
            }
            return result;
        }
     
    }
}