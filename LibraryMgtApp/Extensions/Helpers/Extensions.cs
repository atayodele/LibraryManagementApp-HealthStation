using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Extensions.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        //pagination
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFromatter = new JsonSerializerSettings();
            camelCaseFromatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFromatter));
            response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
        public static DateTime AddBusinessDays(this DateTime dateTime, int businessDays)
        {
            DateTime resultDate = dateTime;
            while (businessDays > 0)
            {
                resultDate = resultDate.AddDays(1);
                if (resultDate.DayOfWeek != DayOfWeek.Saturday &&
                    resultDate.DayOfWeek != DayOfWeek.Sunday)
                    businessDays--;
            }
            return resultDate;
        }

    }
}
