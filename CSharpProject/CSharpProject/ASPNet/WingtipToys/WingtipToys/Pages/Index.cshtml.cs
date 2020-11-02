using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WingtipToys.Pages
{
    public class IndexModel : PageModel
    {
        public string Time { get; set; }
        public string Date { get; set; }

        public void OnGet()
        {
            Time = DateTime.Now.ToShortTimeString();
            Date = DateTime.Today.ToShortDateString();
        }
    }
}
