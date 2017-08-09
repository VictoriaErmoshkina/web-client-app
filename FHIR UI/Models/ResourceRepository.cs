using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace FHIR_UI.Models
{
    /// <summary>
    /// Contains methods to get resources of certain criterias
    /// </summary>
    public class ResourceRepository
    {
        private readonly FhirClient _client;

        /// <summary>
        /// makes connection with server
        /// </summary>
        /// <param name="url">url of Server</param>
        public ResourceRepository(string url)       
        {
            _client = new FhirClient(new Uri(url));            
        }


        /// <summary>
        ///  Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="type">The type of resource to search for</param>
        /// <param name="q">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">>Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <returns>A List with all resource Ids found by the search, or an empty List if none were found.</returns>
        public List<String> FilteredGetIds(string type, int page = 1, string[] q = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null)
        {
            List<String> result = new List<String>();
            if (string.IsNullOrWhiteSpace(type))
                return result;
            var bundle = _client.Search(type );
            while (bundle != null)
            {
                foreach (var entry in bundle.Entry)
                {
                    result.Add(entry.Resource.Id);
                }
                bundle = _client.Continue(bundle);
            }
            return result;
        }

        public Bundle getBunde (string type)
        {
            Bundle result = new Bundle();

            if (string.IsNullOrWhiteSpace(type))
                return result;

            _client.PreferredFormat = ResourceFormat.Json;

            result = _client.Search(type, pageSize: 60);
           var  res = _client.Continue(result);
            return result;

        }

        public Bundle redirect(Bundle current, PageDirection direction)
        {
            try
            {
                var res = _client.Continue(current, direction);
            }catch (Exception ex)
            {
                throw new Exception("action is invalid" );
            }
            return current;
        }

    }    
}
