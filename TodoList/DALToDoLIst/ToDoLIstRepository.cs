
using DALToDoLIst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DALToDoList
{
    public class ToDoListRepository
    {
        public ToDoLIstContext context { get; set; }

        public ToDoListRepository()
        {
            context = new ToDoLIstContext();
        }

        public List<ToDoList> GetAllToDoLists()
        {
            List<ToDoList> list = new List<ToDoList>();
            try
            {
                list = (from lis in context.ToDoLists select lis).ToList();
            }
            catch (Exception e)
            {
                list = null;
                Console.WriteLine(e.Message);
            }
            return list;
        }

        public bool AddToDoList(string description)
        {
            bool status = false;
            try
            {
                ToDoList list = new ToDoList();
                list.Description = description;

                context.ToDoLists.Add(list);
                context.SaveChanges();

                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                Console.WriteLine($"Error adding ToDoList: {ex}");
            }
            return status;
        }




        public bool UpdateToDoList(int id, string description)
        {
            bool status = false;
            try
            {
                var lis = context.ToDoLists.FirstOrDefault(c => c.Id == id);
                if (lis != null)
                {
                    lis.Description = description;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception e)
            {
                status = false;
                Console.WriteLine(e.Message);
            }
            return status;
        }

        public bool DeleteToDoList(int id)
        {
            bool status = false;
            try
            {
                var lis = context.ToDoLists.FirstOrDefault(c => c.Id == id);
                if (lis != null)
                {
                    context.ToDoLists.Remove(lis);
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
