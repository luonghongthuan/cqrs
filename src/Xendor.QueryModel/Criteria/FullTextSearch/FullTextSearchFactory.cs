using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Xendor.QueryModel.Criteria.FullTextSearch
{
    internal class FullTextSearchFactory<TMetaData> : FactoryCriteria<TMetaData, IFullTextSearch<TMetaData>, FullTextSearchEmpty<TMetaData>>
        where TMetaData : IMetaDataCriteria
    {
        public FullTextSearchFactory(IQueryCollection queryCollection)
            : base(queryCollection)
        {
        }

        protected override bool Contains()
        {
            return ContainsKey(FullTextSearchReservedWords.KeyQ);
        }

        protected override bool Validate()
        {
            var fullTextSearch = GetValue(FullTextSearchReservedWords.KeyQ);
            return fullTextSearch.Length.Equals(1);
        }

        protected override IFullTextSearch<TMetaData> Extract()
        {
            var value = GetValue(FullTextSearchReservedWords.KeyQ)[0];
            return new FullTextSearch<TMetaData>(Cache.GetFullTextSearchFields<TMetaData>().Keys.ToList(), value);
        }
    }
}