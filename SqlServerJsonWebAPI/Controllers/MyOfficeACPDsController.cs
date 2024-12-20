using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SqlServerJsonWebAPI.Models;
using YourNamespace.Data;

namespace SqlServerJsonWebAPI.Controllers
{
    public class MyOfficeACPDsController : Controller
    {
        private readonly AppDbContext _context;

        public MyOfficeACPDsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MyOfficeACPDs
        public async Task<IActionResult> Index()
        {
            var data = await _context.MyOfficeACPDs
                .FromSqlRaw("EXEC usp_GetAllACPD")
                .ToListAsync();
            return View(data);
        }

        // GET: MyOfficeACPDs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@ACPD_SID", id);
            var myOfficeACPD = await _context.MyOfficeACPDs
                .FromSqlRaw("EXEC usp_GetACPDById @ACPD_SID", param)
                .FirstOrDefaultAsync();

            if (myOfficeACPD == null)
            {
                return NotFound();
            }

            return View(myOfficeACPD);
        }

        // GET: MyOfficeACPDs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyOfficeACPDs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ACPD_SID,ACPD_Cname,ACPD_Ename,ACPD_Status,ACPD_LoginID,ACPD_LoginPWD,ACPD_NowDateTime")] MyOfficeACPD myOfficeACPD)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(myOfficeACPD);
                var param = new SqlParameter("@JsonData", jsonData);

                await _context.Database.ExecuteSqlRawAsync("EXEC usp_InsertACPD @JsonData", param);
                return RedirectToAction(nameof(Index));
            }
            return View(myOfficeACPD);
        }

        // GET: MyOfficeACPDs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@ACPD_SID", id);
            var myOfficeACPD = await _context.MyOfficeACPDs
                .FromSqlRaw("EXEC usp_GetACPDById @ACPD_SID", param)
                .FirstOrDefaultAsync();

            if (myOfficeACPD == null)
            {
                return NotFound();
            }
            return View(myOfficeACPD);
        }

        // POST: MyOfficeACPDs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ACPD_SID,ACPD_Cname,ACPD_Ename,ACPD_Status,ACPD_LoginID,ACPD_LoginPWD,ACPD_NowDateTime")] MyOfficeACPD myOfficeACPD)
        {
            if (id != myOfficeACPD.ACPD_SID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(myOfficeACPD);
                var param = new SqlParameter("@JsonData", jsonData);

                await _context.Database.ExecuteSqlRawAsync("EXEC usp_UpdateACPD @JsonData", param);
                return RedirectToAction(nameof(Index));
            }
            return View(myOfficeACPD);
        }

        // GET: MyOfficeACPDs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@ACPD_SID", id);
            var myOfficeACPD = await _context.MyOfficeACPDs
                .FromSqlRaw("EXEC usp_GetACPDById @ACPD_SID", param)
                .FirstOrDefaultAsync();

            if (myOfficeACPD == null)
            {
                return NotFound();
            }

            return View(myOfficeACPD);
        }

        // POST: MyOfficeACPDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var param = new SqlParameter("@ACPD_SID", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC usp_DeleteACPD @ACPD_SID", param);

            return RedirectToAction(nameof(Index));
        }

        private bool MyOfficeACPDExists(string id)
        {
            var param = new SqlParameter("@ACPD_SID", id);
            return _context.MyOfficeACPDs
                .FromSqlRaw("EXEC usp_GetACPDById @ACPD_SID", param)
                .Any();
        }
    }
}
