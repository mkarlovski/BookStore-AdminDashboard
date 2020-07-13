using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStoreDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStoreDashboard.Controllers
{
    public class BooksController : Controller
    {

        public async Task<IActionResult> Overview()
        {

            //http request od C#

            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync("https://localhost:44319/api/books");

            if (httpResponse.IsSuccessStatusCode)

            {
                var response = await httpResponse.Content.ReadAsStringAsync(); //go vrakja JSON vo string

                var books = JsonConvert.DeserializeObject<List<BookViewModel>>(response);

                return View(books);
            }


            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync($"https://localhost:44319/api/books/{id}");

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadAsStringAsync(); //go vrakja JSON vo string

                var book = JsonConvert.DeserializeObject<BookViewModel>(response);

                return View(book);
            }


            return RedirectToAction("Error", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel book)
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.PutAsJsonAsync("https://localhost:44319/api/books",book);   //http PUT

            if (httpResponse.IsSuccessStatusCode)
            {
                ViewBag.SuccessMessage = "Updated successfully";

                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Something went wrong";
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.DeleteAsync($"https://localhost:44319/api/books{id}");

            return RedirectToAction(nameof(Overview));  //vaka ako slucajno go smenis endpointot ke se buni
        }
    }  
}