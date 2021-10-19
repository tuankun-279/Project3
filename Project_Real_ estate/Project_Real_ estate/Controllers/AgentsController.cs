using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Real__estate.Models;

namespace Project_Real__estate.Controllers
{
    public class AgentsController : Controller
    {
        private RealEstateEntities1 db = new RealEstateEntities1();

        // GET: Agents
        public ActionResult Index()
        {
            var agents = db.Agents.Include(a => a.User).Include(a => a.Payment);
            return View(agents.ToList());
        }

        // GET: Agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgentId,AgentName,Email,Address,Phone,isActivate,Password,Introduction,EmailHide,paymentId,UserId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgentId,AgentName,Email,Address,Phone,isActivate,Password,Introduction,EmailHide,paymentId,UserId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agent agent = db.Agents.Find(id);
            db.Agents.Remove(agent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
