using System;

namespace ChatChallenge.Core.BaseModelDto.BaseEntityDto
{
    public class BaseEntityDto : BaseDto.BaseDto, IBaseEntityDto
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
