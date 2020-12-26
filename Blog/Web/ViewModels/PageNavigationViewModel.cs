using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Web.ViewModels
{
    public class PageNavigationViewModel
    {
        public UriBuilder BaseUri { get; }

        public string QuerySringKey { get; }

        public int CurrentPageNumber { get; }

        public int MaxPageNumber { get; }

        public PageNavigationViewModel(int currentPageNumber, int maxPageNumber, UriBuilder baseUri, string pageQueryString = "page")
        {
            CurrentPageNumber = currentPageNumber;
            MaxPageNumber = maxPageNumber;
            BaseUri = baseUri;
            QuerySringKey = pageQueryString;
        }

        public bool HasNextPage()
        {
            return CurrentPageNumber < MaxPageNumber;
        }

        public bool HasPrevPage()
        {
            return CurrentPageNumber > 0;
        }

        public string GetLinkByPageNumber(int number)
        {
            VerifyPageNumber(number);

            var queryString = GetQueryString(number);
            BaseUri.Query = queryString;

            return BaseUri.ToString();
        }

        private string GetQueryString(int number)
        {
            var queryCollection = HttpUtility.ParseQueryString(BaseUri.Query);
            queryCollection.Remove(QuerySringKey);
            queryCollection.Add(QuerySringKey, number.ToString());

            var queryString = "?";
            foreach (var key in queryCollection.AllKeys)
            {
                queryString += $"{key}={queryCollection[key]}";
            }

            return queryString;
        }

        private void VerifyPageNumber(int number)
        {
            if (number > MaxPageNumber)
                throw new ArgumentException($"Page for {number} doesn't exists.");

            if (number < 1)
                throw new ArgumentException($"Page for {number} doesn't exists.");
        }
    }
}
