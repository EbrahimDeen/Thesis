using System.ComponentModel.DataAnnotations;

namespace IAM.Data.RequestModels
{
    public class ShareFilePublicRequest: BaseRequestModel
    {
        [Required]
        public int FileId { get; set; }

    }
}
