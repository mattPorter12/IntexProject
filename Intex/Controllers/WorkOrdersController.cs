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
        private NorthwestContext db = new NorthwestContext();
        public int? theClientID;

       
        public ActionResult Index()
        {
            Login name = db.Login.Find(User.Identity.Name);
            Client names = new Client();
            names = db.Client.Find(name.ClientID);
            return View(names);
        }

        public ActionResult Current()
        {
            List<WorkOrder> clientsOrders = new List<WorkOrder>();
            foreach (var item in db.WorkOrder)
            {
                if(item.ClientID == theClientID)
                {
                    clientsOrders.Add(item);
                }
            }

            List<int> list = new List<int>();
           foreach (var item in clientsOrders)
            {
                list.Add(item.OrderStatusID);
            }

            List<OrderStatus> list2 = new List<OrderStatus>();
           foreach (var item in list)
            {
                list2.Add(db.OrderStatus.Find(item));
            }
            ViewBag.OrderStatus = list2;
            return View(db.WorkOrder.ToList());
        }

        public ActionResult Compound(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrders = db.WorkOrder.Find(id);

            List<OrderCompound> list = new List<OrderCompound>();
            foreach(var item in db.OrderCompound)
            {
                if(item.WorkOrderNum == workOrders.WorkOrderNum)
                {
                    list.Add(item);
                }
            }
            List<Compound> list2 = new List<Compound>();
            foreach(var item in db.Compound)
            {
                foreach(var item2 in list)
                if (item.LTNumber == item2.LTNumber)
                    {
                        list2.Add(item);
                    }
            }

            return View(list2);
        }

        public ActionResult Details(int? id, int? id1)
        {
            if (id == null || id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compound.Find(id, id1);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }
        
        
        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.AssayNames = db.Assay.ToList();
            ViewBag.Priority = db.Priority.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LTNumber, SequenceCode, PriorityNumber, " +
                                                    "CompName, AssayID, CompQuantity, ArrivalDate, " +
                                                    "ReceivedBy, EmployeeID, DueDate, CompAppearance, " +
                                                    "CompClientWeight, CompMoleMass, CompMTD, CompActualWeight, " +
                                                    "CompConcentration, IsActive, CompStatusID, UsableResults")] Compound compound)
        {
            WorkOrder workOrders = new WorkOrder();
            Login name = new Login();
            name = db.Login.Find(User.Identity.Name);

            workOrders.ClientID = name.ClientID;
            workOrders.OrderDate = DateTime.Today;
            workOrders.OrderStatusID = 1;

            
                db.WorkOrder.Add(workOrders);
                db.SaveChanges();


                OrderCompound orderCompound = new OrderCompound();

            IEnumerable<WorkOrder> workOrderNum = db.Database.SqlQuery<WorkOrder>(
                "Select * FROM WorkOrder WHERE WorkOrderNum = (SELECT MAX(WorkOrderNum) FROM WorkOrder);");

            int max = 0;
            foreach ( WorkOrder item in workOrderNum)
            {
                if(item.WorkOrderNum > max)
                {
                    max = item.WorkOrderNum;
                }
            }
            orderCompound.WorkOrderNum = max;

                
            db.OrderCompound.Add(orderCompound);
            db.SaveChanges();

            IEnumerable<OrderCompound> LTNumber = db.Database.SqlQuery<OrderCompound>(
                "Select * FROM OrderCompound WHERE LTNumber = (SELECT MAX(LTNumber) FROM OrderCompound);");

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

            compound.SequenceCode = 1;
            compound.CompStatusId = 1;

            db.Compound.Add(compound);
            db.SaveChanges();
            return RedirectToAction("Index");
           // ViewBag.AssayNames = db.Assay.ToList();
            //ViewBag.Priority = db.Priority.ToList();

            //return View(compound);
        }
        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrders = db.WorkOrder.Find(id);
            if (workOrders == null)
            {
                return HttpNotFound();
            }
            return View(workOrders);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkOrderNum,ClientID,OrderDate,OrderStatusID")] WorkOrder workOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workOrders);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrders = db.WorkOrder.Find(id);
            if (workOrders == null)
            {
                return HttpNotFound();
            }
            return View(workOrders);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrders = db.WorkOrder.Find(id);
            db.WorkOrder.Remove(workOrders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
