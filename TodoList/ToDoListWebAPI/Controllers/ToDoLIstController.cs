using DALToDoLIst;
using DALToDoLIst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using DALToDoList;

namespace ToDoListWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoLIstController : Controller
    {
        public ToDoListRepository Repos = new ToDoListRepository();

        [HttpGet]
        public JsonResult GetToDoList()
        {
            List<ToDoList> lis= new List<ToDoList>();
            try
            {
               lis=Repos.GetAllToDoLists();
            }
            catch (Exception e)
            {

                lis = null;
            }
            return Json(lis);
        }
        [HttpPost]
        public JsonResult AddToDoList(string discription)
        {
            bool status = false;

            string message;
            try
            {
                status = Repos.AddToDoList(discription);
                if (status)
                {
                    message = "Successful addition operation";
                }
                else
                {
                    message = "Unsuccessful addition operation!";
                }
            }
            catch (Exception)
            {
                message = "Some error occured, please try again!";
            }
            return Json(message);
        }
        [HttpPatch]
        public JsonResult UpdateToDoList(int id,string discription)
        {
            bool status = false;

            string message;
            try
            {
                status = Repos.UpdateToDoList(id,discription);
                if (status)
                {
                    message = "Successful addition operation";
                }
                else
                {
                    message = "Unsuccessful addition operation!";
                }
            }
            catch (Exception)
            {
                message = "Some error occured, please try again!";
            }
            return Json(message);
        }
        [HttpDelete]
        public JsonResult DeleteToDoList(int id)
        {
            bool status = false;

            string message;
            try
            {
                status = Repos.DeleteToDoList(id);
                if (status)
                {
                    message = "Successful addition operation";
                }
                else
                {
                    message = "Unsuccessful addition operation!";
                }
            }
            catch (Exception)
            {
                message = "Some error occured, please try again!";
            }
            return Json(message);
        }
    }
}
