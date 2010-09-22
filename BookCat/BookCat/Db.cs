using System;
using System.Data;
using System.Data.SQLite;

namespace BookCat
{
    public class Db
    {
        private static SQLiteConnection getActiveCon()
        {
            SQLiteConnection sc = new SQLiteConnection(Program.connString);
            sc.Open();
            return sc;
        }

        public static DataTable Fill(SQLiteCommand sc)
        {
            DataTable dt1 = new DataTable();

            using (SQLiteConnection con = getActiveCon())
            {
				sc.Connection = con;

                SQLiteDataAdapter da = new SQLiteDataAdapter(sc);
                da.Fill(dt1);

				sc.Connection = null;
			}

            return dt1;
        }

        public static long ExecuteNonQueryInsert(SQLiteCommand sc)
        {
            long id;

			if (_transaction == null)
			{
				using (SQLiteConnection con = getActiveCon())
				{
					sc.Connection = con;
					sc.ExecuteNonQuery();
					sc.Connection = null;

					SQLiteCommand sc2 = new SQLiteCommand("SELECT last_insert_rowid()");
					sc2.Connection = con;
					id = (long) sc2.ExecuteScalar();
					sc2.Connection = null;
				}
			}
			else
			{
				sc.Connection = _transaction.Connection;
				sc.Transaction = _transaction;
				sc.ExecuteNonQuery();
				sc.Connection = null;

				SQLiteCommand sc2 = new SQLiteCommand("SELECT last_insert_rowid()");
				sc2.Connection = _transaction.Connection;
				id = (long)sc2.ExecuteScalar();
				sc2.Connection = null;
			}

        	return id;
        }


		static SQLiteTransaction _transaction;

        public static int ExecuteNonQuery(SQLiteCommand sc)
        {
			int ret;
			if (_transaction == null)
			{
				using (SQLiteConnection con = getActiveCon())
				{
					sc.Connection = con;

					ret = sc.ExecuteNonQuery();

					sc.Connection = null;
				}
			}
			else
			{
				sc.Connection = _transaction.Connection;
				sc.Transaction = _transaction;

				ret = sc.ExecuteNonQuery();
			}

			return ret;
		}

        public static object ExecuteScalar(SQLiteCommand sc)
        {
            object i;

            using (SQLiteConnection con = getActiveCon())
            {
				sc.Connection = con;

				i = sc.ExecuteScalar();

				sc.Connection = null;
            }

            return i;
        }

        public static void Vacuum()
        {
            SQLiteCommand sc = new SQLiteCommand("VACUUM");

            using (SQLiteConnection con = getActiveCon())
            {
				sc.Connection = con;

                sc.ExecuteScalar();

				sc.Connection = null;
            }

            return;
        }

		internal static void BeginTransaction()
		{
			_transaction = getActiveCon().BeginTransaction();
		}

    	public static void CommitTransaction()
    	{
			_transaction.Commit();
    		_transaction = null;

    	}
    }
}
