namespace Dashboard.Aggregator.Models.SkillModels
{
    public class GetManyResultModel<T> : ResultModel where T : class, new()
    {
        public IEnumerable<T> Data { get; set; }
    }
}
