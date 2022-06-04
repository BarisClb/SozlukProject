using SozlukProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        //public async Task<int> Get()
        //{
        //    _commentRepository.
        //}

        //public async Task<SortedResponse<IList<CommentReadVm>, ListSortReadVm>> Get(ListSortWriteVm listSorting)
        //{
        //    IList<Comment> comments = _commentRepository.GetAll(false).ToList();
        //    // Sort => Reverse? OrderBy?
        //    IList<Comment> orderedComments;
        //    if (listSorting.Reversed)
        //    {
        //        orderedComments = listSorting.OrderBy switch
        //        {
        //            "UserUsername" => comments.OrderByDescending(c => c.UserUsername).ToList(),
        //            "ProductName" => comments.OrderByDescending(c => c.ProductName).ToList(),
        //            "Rating" => comments.OrderByDescending(c => c.Rating).ToList(),
        //            _ => comments.Reverse().ToList(),
        //        };
        //    }
        //    else
        //    {
        //        orderedComments = listSorting.OrderBy switch
        //        {
        //            "UserUsername" => comments.OrderBy(c => c.UserUsername).ToList(),
        //            "ProductName" => comments.OrderBy(c => c.ProductName).ToList(),
        //            "Rating" => comments.OrderBy(c => c.Rating).ToList(),
        //            _ => comments,
        //        };
        //    }
        //    // Pagination and Mapping
        //    if (listSorting.PageSize == 0)
        //        listSorting.PageSize = comments.Count;

        //    IList<CommentReadVm> mappedComments = orderedComments.Skip((listSorting.PageNumber - 1) * listSorting.PageSize).Take(listSorting.PageSize).Select(comment => new CommentReadVm
        //    {
        //        Id = comment.Id,
        //        Title = comment.Title,
        //        Text = comment.Text,
        //        Rating = comment.Rating,
        //        ProductName = comment.ProductName,
        //        ProductId = comment.ProductId,
        //        UserUsername = comment.UserUsername,
        //        UserId = comment.UserId,
        //        DateCreated = comment.DateCreated,
        //        DateUpdated = comment.DateUpdated,
        //    }).ToList();

        //    return new SortedResponse<IList<CommentReadVm>, ListSortReadVm>(mappedComments, new ListSortReadVm(listSorting.SearchWord, listSorting.PageNumber, listSorting.PageSize, comments.Count, listSorting.Reversed, listSorting.OrderBy));
        //}

        //public async Task<BaseResponse> Get(int id)
        //{
        //    Comment comment = await _commentRepository.GetByIdAsync(id, false);
        //    if (comment == null)
        //        return new FailResponse("Comment does not exist.");

        //    CommentReadVm mappedComment = new()
        //    {
        //        Id = comment.Id,
        //        Title = comment.Title,
        //        Text = comment.Text,
        //        Rating = comment.Rating,
        //        ProductName = comment.ProductName,
        //        ProductId = comment.ProductId,
        //        UserUsername = comment.UserUsername,
        //        UserId = comment.UserId,
        //        DateCreated = comment.DateCreated,
        //        DateUpdated = comment.DateUpdated,
        //    };

        //    return new SuccessfulResponse<CommentReadVm>(mappedComment);
        //}

        //public async Task<BaseResponse> ByProduct(int id)
        //{
        //    if (await _productRepository.GetByIdAsync(id, false) == null)
        //        return new FailResponse("Product does not exist.");

        //    IList<Comment> comments = _commentRepository.GetWhere(comment => comment.ProductId == id, false).ToList();
        //    IList<CommentReadVm> mappedComments = comments.Select(comment => new CommentReadVm
        //    {
        //        Id = comment.Id,
        //        Title = comment.Title,
        //        Text = comment.Text,
        //        Rating = comment.Rating,
        //        ProductName = comment.ProductName,
        //        ProductId = comment.ProductId,
        //        UserId = comment.UserId,
        //        UserUsername = comment.UserUsername,
        //        DateCreated = comment.DateCreated,
        //        DateUpdated = comment.DateUpdated,
        //    }).ToList();

        //    return new SuccessfulResponse<IList<CommentReadVm>>(mappedComments);
        //}

        //public async Task<BaseResponse> ByUser(int id, ListSortWriteVm listSorting)
        //{
        //    if (await _userRepository.GetByIdAsync(id, false) == null)
        //        return new FailResponse("User does not exist.");

        //    IList<Comment> comments = _commentRepository.GetWhere(comment => comment.UserId == id, false).ToList();
        //    // Sort => Reverse? OrderBy?
        //    IList<Comment> orderedComments;
        //    if (listSorting.Reversed)
        //    {
        //        orderedComments = listSorting.OrderBy switch
        //        {
        //            "UserUsername" => comments.OrderByDescending(c => c.UserUsername).ToList(),
        //            "ProductName" => comments.OrderByDescending(c => c.ProductName).ToList(),
        //            "Rating" => comments.OrderByDescending(c => c.Rating).ToList(),
        //            _ => comments.Reverse().ToList(),
        //        };
        //    }
        //    else
        //    {
        //        orderedComments = listSorting.OrderBy switch
        //        {
        //            "UserUsername" => comments.OrderBy(c => c.UserUsername).ToList(),
        //            "ProductName" => comments.OrderBy(c => c.ProductName).ToList(),
        //            "Rating" => comments.OrderBy(c => c.Rating).ToList(),
        //            _ => comments,
        //        };
        //    }
        //    // Pagination and Mapping
        //    if (listSorting.PageSize == 0)
        //        listSorting.PageSize = comments.Count;

        //    IList<CommentReadVm> mappedComments = orderedComments.Skip((listSorting.PageNumber - 1) * listSorting.PageSize).Take(listSorting.PageSize).Select(comment => new CommentReadVm
        //    {
        //        Id = comment.Id,
        //        Title = comment.Title,
        //        Text = comment.Text,
        //        Rating = comment.Rating,
        //        ProductName = comment.ProductName,
        //        ProductId = comment.ProductId,
        //        UserUsername = comment.UserUsername,
        //        UserId = comment.UserId,
        //        DateCreated = comment.DateCreated,
        //        DateUpdated = comment.DateUpdated,
        //    }).ToList();

        //    return new SortedResponse<IList<CommentReadVm>, ListSortReadVm>(mappedComments, new ListSortReadVm(listSorting.SearchWord, listSorting.PageNumber, listSorting.PageSize, comments.Count, listSorting.Reversed, listSorting.OrderBy));
        //}

        //public async Task<BaseResponse> Post(CommentCreateVm modelComment)
        //{
        //    Product product = await _productRepository.GetByIdAsync(modelComment.ProductId);
        //    if (product == null)
        //        return new FailResponse("Product does not exist.");

        //    User user = await _userRepository.GetByIdAsync(modelComment.UserId);
        //    if (user == null)
        //        return new FailResponse("User does not exist.");

        //    // Checking if the Information is Empty
        //    if (String.IsNullOrWhiteSpace(modelComment.Title))
        //        return new FailResponse("Invalid Title.");
        //    if (String.IsNullOrWhiteSpace(modelComment.Text))
        //        return new FailResponse("Invalid Text.");

        //    if (modelComment.Rating < 0)
        //        modelComment.Rating = 0;
        //    if (modelComment.Rating > 5)
        //        modelComment.Rating = 5;

        //    // Trim and Replace Multiple Whitespaces
        //    modelComment.Title = EntityValidator.TrimAndReplaceMultipleWhitespaces(modelComment.Title);
        //    modelComment.Text = EntityValidator.TrimAndReplaceMultipleWhitespaces(modelComment.Text);

        //    await _commentRepository.AddAsync(new()
        //    {
        //        Title = modelComment.Title,
        //        Text = modelComment.Text,
        //        Rating = modelComment.Rating,
        //        ProductName = product.Name,
        //        Product = product,
        //        UserUsername = user.Username,
        //        User = user
        //    });

        //    await _commentRepository.SaveAsync();
        //    return new SuccessfulResponse<Comment>("Comment created.");
        //}

        //public async Task<BaseResponse> Put(CommentUpdateVm modelComment)
        //{
        //    Comment comment = await _commentRepository.GetByIdAsync(modelComment.Id);
        //    if (comment == null)
        //        return new FailResponse("Comment does not exist.");

        //    if (modelComment.Title != null)
        //    {
        //        modelComment.Title = EntityValidator.TrimAndReplaceMultipleWhitespaces(modelComment.Title);
        //        if (modelComment.Title == "")
        //            return new FailResponse("Invalid Title.");

        //        comment.Title = modelComment.Title;
        //    }
        //    if (modelComment.Text != null)
        //    {
        //        modelComment.Text = EntityValidator.TrimAndReplaceMultipleWhitespaces(modelComment.Text);
        //        if (modelComment.Text == "")
        //            return new FailResponse("Invalid Text.");

        //        comment.Text = modelComment.Text;
        //    }
        //    if (modelComment.Rating != null)
        //        comment.Rating = (byte)modelComment.Rating;

        //    // I put this here so that it checks the rating even if the modelComment.Rating is null (even if it's not updated manualy).
        //    if (modelComment.Rating < 0)
        //        modelComment.Rating = 0;
        //    if (comment.Rating > 5)
        //        comment.Rating = 5;

        //    await _commentRepository.SaveAsync();
        //    return new SuccessfulResponse<Comment>("Comment updated.");
        //}

        //public async Task<BaseResponse> Delete(int id)
        //{
        //    if (await _commentRepository.GetByIdAsync(id, false) == null)
        //        return new FailResponse("Comment does not exist.");

        //    await _commentRepository.RemoveAsync(id);
        //    await _commentRepository.SaveAsync();
        //    return new SuccessfulResponse<Comment>("Comment deleted.");
        //}
    }
}
