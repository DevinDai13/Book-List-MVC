using System;

//  every table in the database, we will have a model file in this folder
namespace BookListMVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
