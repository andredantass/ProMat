using ProMat.WebAPI.Model;
using ProMat.WebAPI.Service;
using System;
using System.Collections.Generic;

namespace ProMatExecuteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitrix24Services objService = new Bitrix24Services();
            AttendantServices attedantasService = new AttendantServices();
            FormServices formService = new FormServices();

            //objService.CreateLead("Silvia Aparecida", 12, "Silvia Aparecida", "1198987787", "andre.dantass@outlook.com","NEW");
            //objService.CreateLead("Amanda Nunes", 12, "Amanda Nunes", "1198987787", "amanda.nunces@outlook.com", "1");
            //objService.CreateLead("Renata dos Santos", 12, "Renata dos Santos", "1198987787", "renata.dossantos@outlook.com", "IN_PROCESS");
            //objService.GetDepartments();
            //List<Attendant> lstAttendants = attedantasService.GetAttendants();
            objService.GetEmployeeDepartments();
            //List<string> lstString = formService.GetNextUserDepartmentLeadDisQualifield();
            Console.WriteLine("Hello World!");
        }
    }
}
