using System;
namespace ChatChallenge.Core.BaseModelDto.BaseDto
{
    public interface IBaseDto
    {
        int? Id { get; set; }
        bool Deleted { get; set; }
    }
}
