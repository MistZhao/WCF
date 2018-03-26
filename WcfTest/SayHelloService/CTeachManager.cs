using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Common;
using System.Data.SqlClient;
using System.Data;

namespace SayHelloService
{
    class CTeachManager : ITeachManager
    {
        private Guid[][] ;

        public CTeachManager()
        {
            dicTeachToClass = new Dictionary<Guid, Guid>();
            DbHelper.InitConnString();
        }

        private bool GetTeachRelation()
        {
            string strSql = "select * from TeacherToClass";
            DataTable dt = DbHelper.ExecuteDataTable(strSql);
            foreach (DataRow row in dt.Rows)
            {
                Guid gidTeacherId = (Guid)row["TeacherId"];
                Guid gidClassId = (Guid)row["ClassId"];
                if (dicTeachToClass.Contains(new KeyValuePair(gidTeacherId, gidClassId)))
                {

                }
            }
        }
    }
}
