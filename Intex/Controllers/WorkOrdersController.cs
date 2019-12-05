using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex.DAL;
using Intex.Models;

namespace Intex.Controllers
{
    public class WorkOrdersController : AuthorizedController
    {
        //initialize db of type NorthwestContext
        private NorthwestContext db = new NorthwestContext();

        //initialize theOrder list of type compound
        public static List<Compound> theOrder = new List<Compound>();

        //create dictionary with compound identifier numbers
        public static Dictionary<string, int> theDict = new Dictionary<string, int>()
        {
            { "LTNumber", 0 },
            { "SeqNum", 0 }
        };

       //locate and pass client name to the index view through a client object
        public ActionResult Index()
        {
            Login name = db.Login.Find(User.Identity.Name);
            Client names = new Client();
            names = db.Client.Find(name.ClientID);
            return View(names);
        }

        //display orderstatus values and orderdate to current view
        public ActionResult Current()
        {
            Login theClient = db.Login.Find(User.Identity.Name);
            int clientNum = theClient.ClientID;
            //query the database to identify client id with incomplete order status
            IEnumerable<WorkOrder> clientsOrders = db.Database.SqlQuery<WorkOrder>("SELECT * FROM WorkOrder WHERE ClientID = " + clientNum + " AND OrderStatusID != 6;");
            
            //initialize list and dates lists
            List<int> list = new List<int>();
            List<string> dates = new List<string>();

            //add order status and order date to initialized lists
           foreach (var item in clientsOrders)
            {
                list.Add(item.OrderStatusID);
                dates.Add(item.OrderDate.Date.ToShortDateString());
            }

            //initialize list2 list of type orderstatus
            List<OrderStatus> list2 = new List<OrderStatus>();

            //add all orderstatus values within db from list to list2
           foreach (var item in list)
            {
                list2.Add(db.OrderStatus.Find(item));
            }

           //display list values to viewbag
            ViewBag.dates = dates;
            ViewBag.OrderStatus = list2;
            return View(clientsOrders.ToList());
        }

        //display dates, assay, and compound information to compound view
        public ActionResult Compound(int? id)
        {
            //check for valid id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //locate client workorders based on id and display to viewbag
            WorkOrder workOrders = db.WorkOrder.Find(id);
            ViewBag.WorkOrder = workOrders;

            ////initialize list list of type ordercompound
            List<OrderCompound> list = new List<OrderCompound>();

            //add values to list based on workorder number in workOrders object
            foreach(var item in db.OrderCompound)
            {
                if(item.WorkOrderNum == workOrders.WorkOrderNum)
                {
                    list.Add(item);
                }
            }

            //initialize list2, dates, theAssays, and theCompStatus lists
            List<Compound> list2 = new List<Compound>();
            List<string> dates = new List<string>();
            List<Assay> theAssays = new List<Assay>();
            List<CompoundStatus> theCompStatus = new List<CompoundStatus>();

            //nested foreach statement checks for matching LTNumber values in db and list and adds values to list2 and dates
            foreach (var item in db.Compound)
            {
                foreach(var item2 in list)
                if (item.LTNumber == item2.LTNumber)
                    {
                        list2.Add(item);
                        dates.Add(item.DueDate.ToShortDateString());
                    }
            }

            //locate AssayID for each item in list2 and add value to theAssays list
            foreach(var item in list2)
            {
                theAssays.Add(db.Assay.Find(item.AssayID));
            }

            //add compound status values to theCompStatus list from CompStatusID values within list2
            foreach(var item in list2)
            {
                theCompStatus.Add(db.CompoundStatus.Find(item.CompStatusID));
            }

            //display dates, theAssays, and theCompStatus lists to viewbag
            ViewBag.dates = dates;
            ViewBag.theAssays = theAssays;
            ViewBag.CompStatus = theCompStatus;
            return View(list2);
        }

        //return compound ids to details view
        public ActionResult Details(int? id, int? id1)
        {
            //check for valid id
            if (id == null || id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //initialize compound object holding ids
            Compound compound = db.Compound.Find(id, id1);

            //check for valid compound value
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }
        
        //display assay and priority values to viewbag for client work order creation
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.AssayNames = db.Assay.ToList();
            ViewBag.Priority = db.Priority.ToList();
            return View();
        }

        //store client created work order values
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PriorityNumber, " +
                                                    "CompName, AssayID, CompQuantity, ArrivalDate, " +
                                                    "ReceivedBy, EmployeeID, DueDate, CompAppearance, " +
                                                    "CompClientWeight, CompMoleMass, CompMTD, CompActualWeight, " +
                                                    "CompConcentration, IsActive, CompStatusID, UsableResults")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                if (theDict["LTNumber"] == 0)
                {
                    //initialize workOrders object and check proper login
                    WorkOrder workOrders = new WorkOrder();
                    Login name = new Login();
                    name = db.Login.Find(User.Identity.Name);

                    //add values to workOrders object and save new work order
                    workOrders.ClientID = name.ClientID;
                    workOrders.OrderDate = DateTime.Today;
                    workOrders.OrderStatusID = 1;

                    db.WorkOrder.Add(workOrders);
                    db.SaveChanges();

                    //initialize orderCompound object
                    OrderCompound orderCompound = new OrderCompound();

                    //query database to find all of the maximum work order numbers
                    IEnumerable<WorkOrder> workOrderNum = db.Database.SqlQuery<WorkOrder>(
                        "Select * FROM WorkOrder WHERE WorkOrderNum = (SELECT MAX(WorkOrderNum) FROM WorkOrder);");

                    //assign maximum workOrderNum to max variable
                    int max = 0;
                    foreach (WorkOrder item in workOrderNum)
                    {
                        if (item.WorkOrderNum > max)
                        {
                            max = item.WorkOrderNum;
                        }
                    }
                    orderCompound.WorkOrderNum = max;


                    db.OrderCompound.Add(orderCompound);
                    db.SaveChanges();

                    //query database to find all of the maximum lt numbers
                    IEnumerable<OrderCompound> LTNumber = db.Database.SqlQuery<OrderCompound>(
                        "Select * FROM OrderCompound WHERE LTNumber = (SELECT MAX(LTNumber) FROM OrderCompound);");

                    //assign maximum LTNumber to max variable
                    max = 0;
                    foreach (OrderCompound item in LTNumber)
                    {
                        if (item.LTNumber > max)
                        {
                            max = item.LTNumber;
                        }
                    }
                    orderCompound.LTNumber = max;

                    compound.LTNumber = max;
                    //set sequence code and status id to 1
                    compound.SequenceCode = 1;
                    compound.CompStatusID = 1;

                    //add values above to compound values
                    db.Compound.Add(compound);
                    theOrder.Add(compound);
                    db.SaveChanges();
                    theDict["LTNumber"] = 0;
                    theDict["SeqNum"] = 0;
                    return RedirectToAction("ThankYou");
                }
                else
                {
                    //set and save lt numbers and sequence numbers to the created work order
                    compound.LTNumber = theDict["LTNumber"];
                    compound.SequenceCode = theDict["SeqNum"] + 1;
                    compound.CompStatusID = 1;

                    db.Compound.Add(compound);
                    theOrder.Add(compound);
                    db.SaveChanges();
                    theDict["LTNumber"] = 0;
                    theDict["SeqNum"] = 0;
                    return RedirectToAction("ThankYou");
                }

            }
            //display assay and priority values to viewbag
           ViewBag.AssayNames = db.Assay.ToList();
           ViewBag.Priority = db.Priority.ToList();

            return View(compound);
        }

        //post method saves multiple work orders to client 
        public ActionResult CreateMultiple([Bind(Include = "PriorityNumber, " +
                                                    "CompName, AssayID, CompQuantity, ArrivalDate, " +
                                                    "ReceivedBy, EmployeeID, DueDate, CompAppearance, " +
                                                    "CompClientWeight, CompMoleMass, CompMTD, CompActualWeight, " +
                                                    "CompConcentration, IsActive, CompStatusID, UsableResults")] Compound compound)
         {
            if (ModelState.IsValid)
            {
                if (theDict["LTNumber"] == 0)
                {
                    //initialize workOrders object and check proper login
                    WorkOrder workOrders = new WorkOrder();
                    Login name = new Login();
                    name = db.Login.Find(User.Identity.Name);

                    //add values to workOrders object and save new work order
                    workOrders.ClientID = name.ClientID;
                    workOrders.OrderDate = DateTime.Today;
                    workOrders.OrderStatusID = 1;

                    db.WorkOrder.Add(workOrders);
                    db.SaveChanges();

                    //initialize orderCompound object
                    OrderCompound orderCompound = new OrderCompound();

                    //query database to find all of the maximum work order numbers
                    IEnumerable<WorkOrder> workOrderNum = db.Database.SqlQuery<WorkOrder>(
                        "Select * FROM WorkOrder WHERE WorkOrderNum = (SELECT MAX(WorkOrderNum) FROM WorkOrder);");

                    //assign maximum workOrderNum to max variable
                    int max = 0;
                    foreach (WorkOrder item in workOrderNum)
                    {
                        if (item.WorkOrderNum > max)
                        {
                            max = item.WorkOrderNum;
                        }
                    }
                    orderCompound.WorkOrderNum = max;

                    db.OrderCompound.Add(orderCompound);
                    db.SaveChanges();

                    //query database to find all of the maximum lt numbers
                    IEnumerable<OrderCompound> LTNumber = db.Database.SqlQuery<OrderCompound>(
                   "Select * FROM OrderCompound WHERE LTNumber = (SELECT MAX(LTNumber) FROM OrderCompound);");

                    //assign maximum LTNumber to max variable
                    max = 0;
                    foreach (OrderCompound item in LTNumber)
                    {
                        if (item.LTNumber > max)
                        {
                            max = item.LTNumber;
                        }
                    }
                    orderCompound.LTNumber = max;

                    compound.LTNumber = max;
                    //add values above to compound values
                    compound.SequenceCode = 1;
                    compound.CompStatusID = 1;

                    //set and save compound object to the created work order
                    db.Compound.Add(compound);
                    theOrder.Add(compound);
                    db.SaveChanges();
                }
                else
                {
                    //set and save lt numbers and sequence numbers to the created work order
                    compound.LTNumber = theDict["LTNumber"];
                    compound.SequenceCode = theDict["SeqNum"] + 1;
                    compound.CompStatusID = 1;

                    db.Compound.Add(compound);
                    theOrder.Add(compound);
                    db.SaveChanges();

                }
                theDict["LTNumber"] = compound.LTNumber;
                theDict["SeqNum"] = compound.SequenceCode;
                ViewBag.AssayNames = db.Assay.ToList();
                ViewBag.Priority = db.Priority.ToList();

                return View("Create");
            }
            //display assay and priority information to viewbag
            ViewBag.AssayNames = db.Assay.ToList();
            ViewBag.Priority = db.Priority.ToList();
            return View("Create", compound);
        }

        //display new work order information
        public ActionResult ThankYou()
        {
            //initialize theActualOrder list of type compound
            List<Compound> theActualOrder = new List<Compound>();
            theActualOrder = theOrder;
            theOrder = null;

            //initialize theAssays, thePriority, and dates lists
            List<Assay> theAssays = new List<Assay>();
            List<Priority> thePriority = new List<Priority>();
            List<string> dates = new List<string>();

            //add due date and assay information to dates and theAssays lists
            foreach(var item in theActualOrder)
            {
                dates.Add(item.DueDate.ToShortDateString());
                theAssays.Add(db.Assay.Find(item.AssayID));
            }
            //add priority information form theActualOrder to thePriority 
            foreach(var item in theActualOrder)
            {
                thePriority.Add(db.Priority.Find(item.PriorityNumber));
            }

            //display dates, theAssays, and thePriority lists to viewbag
            ViewBag.dates = dates;
            ViewBag.theAssays = theAssays;
            ViewBag.thePrior = thePriority;
            return View(theActualOrder);
        }

        //display past work orders to PastOrder view
        public ActionResult PastOrders()
        {
            //check login information
            Login name = new Login();
            name = db.Login.Find(User.Identity.Name);
            int clientId = name.ClientID;
            //query database to find work order information
            IEnumerable<WorkOrder> pastOrders = db.Database.SqlQuery<WorkOrder>("SELECT * " +
                                                                                "FROM WorkOrder " +
                                                                                "WHERE ClientID = 1 " +
                                                                                "AND OrderStatusID = 6 " +
                                                                                "ORDER BY OrderDate;");
            //initialize listpast, list, and dates lists
            List<WorkOrder> listpast = new List<WorkOrder>();
            listpast = pastOrders.ToList();
            List<OrderStatus> list = new List<OrderStatus>();
            List<string> dates = new List<string>();

            //add OrderStatus and OrderDate information to dates and list string
            foreach(var item in listpast)
            {
                list.Add(db.OrderStatus.Find(item.OrderStatusID));
                dates.Add(item.OrderDate.Date.ToShortDateString());
            }

            //display dates and list to viewbag
            ViewBag.dates = dates;
            ViewBag.OrderStatus = list;

            return View(pastOrders.ToList());
        }

        //display past compound information to PastCompounds view
        public ActionResult PastCompounds(int? id)
        {
            //check for valid id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrders = db.WorkOrder.Find(id);
            ViewBag.WorkOrder = workOrders;

            //initialize list and add WorderOrderNum information to list
            List<OrderCompound> list = new List<OrderCompound>();
            foreach (var item in db.OrderCompound)
            {
                if (item.WorkOrderNum == workOrders.WorkOrderNum)
                {
                    list.Add(item);
                }
            }

            //initialize list2, dates, theAssays, and theCompStatus lsits
            List<Compound> list2 = new List<Compound>();
            List<string> dates = new List<string>();
            List<Assay> theAssays = new List<Assay>();
            List<CompoundStatus> theCompStatus = new List<CompoundStatus>();

            //add database information to initialized list
            foreach (var item in db.Compound)
            {
                foreach (var item2 in list)
                    if (item.LTNumber == item2.LTNumber)
                    {
                        list2.Add(item);
                        dates.Add(item.DueDate.ToShortDateString());
                    }
            }
            foreach (var item in list2)
            {
                theAssays.Add(db.Assay.Find(item.AssayID));
            }
            foreach (var item in list2)
            {
                theCompStatus.Add(db.CompoundStatus.Find(item.CompStatusID));
            }

            //display dates, theAssays, and theCompStatus lists to viewbag
            ViewBag.dates = dates;
            ViewBag.theAssays = theAssays;
            ViewBag.CompStatus = theCompStatus;
            return View(list2);
        }
        
        //display compount information to resutls view
        public ActionResult Results(int?id, int?id2)
        {
            Compound theCompound = db.Compound.Find(id, id2);
            return View(theCompound);
        }
    }
}
