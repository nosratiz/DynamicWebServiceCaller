using System.ComponentModel.DataAnnotations;

namespace WebServiceCaller.Common.General
{
    public class PagingOptions
    {
        [Range(1, 9999)] public int Page { get; set; } = 1;

        [Range(1, 100)] public int Limit { get; set; } = 10;

        public string Query { get; set; }
    }
}