﻿
namespace Skill.Domain.Entities.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class GetOneResult<T> : Result where T : SkillBase, new()
    {
        public T Data { get; set; }
    }

    public class GetManyResult<T> : Result where T : SkillBase, new()
    {
        public IEnumerable<T> Data { get; set; }
    }
}
