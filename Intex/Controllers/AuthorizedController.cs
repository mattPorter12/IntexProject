using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intex.Controllers
{
    //invoice and work order controllers inherit from authroized controller to create cookies 
    [Authorize]
    public class AuthorizedController : Controller
    {
        
    }
}