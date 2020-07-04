using ChatChallenge.Core.BaseModelDto.BaseDto;

namespace ChatChallenge.Core.BaseModelDto.BaseEntityDto
{
    public interface IBaseEntityDto : IBaseDto
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
    }
}
