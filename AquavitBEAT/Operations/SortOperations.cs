using AquavitBEAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AquavitBEAT.DbServices;

namespace AquavitBEAT.Operations
{
    public class SortOperations
    {
        private AquavitDbService _dbService = new AquavitDbService();

        public List<Release> SortReleases(string allReleases)
        {
            
            var sortedReleases = new List<Release>();

            switch (allReleases)
            {
                case "1":
                    sortedReleases = _dbService.OrderReleasesByTitle();
                    break;
                case "2":
                    sortedReleases = _dbService.OrderReleasesByTitleDecending();
                    break;
                case "3":
                    sortedReleases = _dbService.OrderByReleaseDate();
                    break;
                case "4":
                    sortedReleases = _dbService.OrderByReleaseDateDecending();
                    break;
                default:
                    break;
            }
            return sortedReleases;
        }
    }
}