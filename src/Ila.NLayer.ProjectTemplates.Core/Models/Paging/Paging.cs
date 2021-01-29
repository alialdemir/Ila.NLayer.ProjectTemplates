using Newtonsoft.Json;

namespace Ila.NLayer.ProjectTemplates.Core.Models.Paging
{
    public class Paging : IPaging
    {
        private int? _pageNumber = 1;

        private int? _pageSize = 10;

        [JsonIgnore]
        public int Offset
        {
            get
            {
                return PageSize.Value * (PageNumber.Value - 1);
            }
        }

        /// <summary>
        /// Page number
        /// </summary>
        public int? PageNumber
        {
            get { return _pageNumber; }
            set { if (value > 0) _pageNumber = value; }
        }

        /// <summary>
        /// Page size
        /// </summary>
        public int? PageSize
        {
            get { return _pageSize; }
            set { if (value > 0) _pageSize = value; }
        }

        public override string ToString()
        {
            return $"?{nameof(PageSize)}={PageSize}&{nameof(PageNumber)}={PageNumber}";
        }
    }
}