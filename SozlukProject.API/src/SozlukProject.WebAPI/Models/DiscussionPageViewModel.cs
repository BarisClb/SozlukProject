using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Shared;

namespace SozlukProject.WebAPI.Models
{
    public class DiscussionPageViewModel
    {
        public DiscussionPageReadDto DiscussionPageData { get; set; }
        public EntityListSortValues SortValues { get; set; }
    }
}
