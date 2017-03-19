﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace AzureAssignment.Services
{
    public class AzureSearch
    {
        private static SearchServiceClient searchClient;
        private static SearchIndexClient indexClient;

        public static string errorMessage;

        static AzureSearch()
        {
            try
            {
                string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
                string apiKey = ConfigurationManager.AppSettings["SearchServiceApiKey"];

                // Create an HTTP reference to the catalog index
                searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
                indexClient = searchClient.Indexes.GetClient("azure-index");
            }
            catch (Exception e)
            {
                errorMessage = e.Message.ToString();
            }
        }

        public DocumentSearchResult Search(string searchText)
        {
            // Execute search based on query string
            try
            {
                SearchParameters sp = new SearchParameters() { SearchMode = SearchMode.All };
               return indexClient.Documents.Search(searchText, sp);
            }
            catch (Exception ex)
            {
                throw new Exception("Error querying index: {0}\r\n" + ex.Message.ToString());
            }

            return null;
        }
    }
}