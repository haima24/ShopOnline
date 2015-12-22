using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;
using ShopOnline.Service;
using ShopOnline.Extension;
namespace ShopOnline.Controllers
{
    public class CommentController : BaseFrontController
    {
        private readonly CommentService _commentService;
        public CommentController()
        {
            _commentService = new CommentService();
        }
        public ActionResult Reply(int parentId, string text)
        {
            var result = _commentService.CreateComment(text, UserId ?? 0, null, parentId);
            var commentModel = new CommentViewModel();
            var comment = _commentService.GetComment(result.CommentId);
            if (comment != null)
            {
                commentModel = AutoMapper.Mapper.Map<CommentViewModel>(comment);
            }
            var html = this.RenderPartialViewToString("ReplySection", commentModel);
            return Json(new { result = comment != null, html });
        }

        public ActionResult CreateComment(string detail, int productId)
        {
            var result = _commentService.CreateComment(detail, UserId ?? 0, productId, null);
            var commentModel = new CommentViewModel();
            var comment = _commentService.GetComment(result.CommentId);
            if (comment != null)
            {
                commentModel = AutoMapper.Mapper.Map<CommentViewModel>(comment);
            }
            var commentCount = _commentService.GetCommentCountByProduct(productId);
            ViewBag.UserId = UserId;
            var html = this.RenderPartialViewToString("CommentBlock", commentModel);
            return Json(new { result = comment != null, commentCount, html });
        }

        public ActionResult RenderCommentSection()
        {
            return PartialView(UserId);
        }


        public ActionResult RenderSignUpToCommentLink()
        {
            return PartialView(UserId);
        }
        public ActionResult UpdateComment(string detail, int commentId)
        {
            var result = _commentService.UpdateComment(detail, commentId, UserId ?? 0);
            return Json(new { result });
        }
        public ActionResult GetComments(int page,int pageSize,int productId)
        {
            var isLastPage = false;
            var commentsCount = 0;
            var comments = _commentService.GetComments(page, pageSize,out isLastPage,out commentsCount, productId);
            var commentsModel = AutoMapper.Mapper.Map<List<CommentViewModel>>(comments);
            ViewBag.UserId = UserId;
            var html = this.RenderPartialViewToString("GetComments", commentsModel);
            return Json(new {html, isLastPage, commentsCount});
        }
    }
}
