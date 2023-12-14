using QLSANBONG.DAO;
using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.DataHandle
{
    public class JobDAO
    {
        public List<Job> loadJobList()
        {
            List<Job> JobList = new List<Job>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getJob();
            foreach (DataRow item in dt.Rows)
            {
                Job Job = new Job(item);
                JobList.Add(Job);
            }
            db.closeConnection();
            return JobList;
            
        }
        public Job DAOgetJobByID(int id)
        {
            Job job = new Job();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getJobById(id);   
            foreach (DataRow item in dt.Rows)
            {
                job = new Job(item);
                return job;
            }
            db.closeConnection();
            return job;
            
        }
    }
}
