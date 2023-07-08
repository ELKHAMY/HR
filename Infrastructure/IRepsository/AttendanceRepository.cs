using Domain.Models;
using Infrastructure.Data;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepsository
{
    public class AttendanceRepository : IAttendanceRepository

    {
        HRAppDbContext db;


        public AttendanceRepository(HRAppDbContext _db)
        {

            this.db = _db;

        }



        public List<AttendanceViewModel> GetAll()
        {
            var r = db.Attendance
               .Include(a => a.EmployeePersonalData)
               .Select(a => new AttendanceViewModel
               {
                   id = a.Id,
                   attend = a.Attend,
                   Departure = a.Departure,
                   employeeId = a.EmployeeId,
                   employeeName = a.EmployeePersonalData.Name,
                   department = a.EmployeePersonalData.Department.Name
               })
               .ToList();
            return r;
        }


        public void Insert(Attendance attend)
        {
            db.Attendance.Add(attend);
        }

        public void Delete(int id)
        {
            var attend = GetById(id);
            if (attend != null)
            {
                db.Remove(attend);
            }
        }


        public Attendance GetById(int id)
        {
            return db.Attendance
                .Include(a => a.EmployeePersonalData)
                .FirstOrDefault(d => id == d.Id);
        }



        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Attendance attend)
        {
            Attendance attendance = GetById(attend.Id);
            if (attendance != null)
            {
                attendance.Attend = attend.Attend;
                attendance.Departure = attend.Departure;
                attendance.EmployeeId = attend.EmployeeId;
                db.SaveChanges(); // save changes to database
            }
        }



    }
}

