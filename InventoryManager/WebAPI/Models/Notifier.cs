using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Notifier : INotifier
    {
        public void Notify(string message)
        {
            //Don't do anything because this is a mock. 
        }
    }
}