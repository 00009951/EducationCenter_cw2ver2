using EducationCenter_cw2.Models:
using System;
using System.Collections.Generic;

namespace EducationCenter_cw2
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Filter(
        string firstName, 
        string lastName, 
        DateTime? birthDate,
        string sortColumn = null,
        bool sortDeck= false,
        Int page = 1,
        Int pageSize = 5);
    {             
            #region Filtration
            string sqlWhere = "";
            var parameters = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                sqlWhere += " FirstName like @FirstName + '%' AND ";
                parameters.Add("@FirstName", firstName);
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                sqlWhere += " LastName like @LastName + '%' AND ";
                parameters.Add("@LastName", lastName);
            }

            if (dob != null)
            {
                sqlWhere += " BirthDate >= @BirthDate AND ";
                parameters.Add("@BirthDate", dob);
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlWhere = " WHERE "
                    + sqlWhere.Substring(0, sqlWhere.Length - 4);
            }

            #endregion Filtration

            #region Paging
            string sqlCountTotal = string.Format(SQL_FILTER, sqlWhere, "", " count(*) ", "");

            int offsetRows = (page - 1) * pageSize;
            if (offsetRows < 0)
                offsetRows = 0;
            string sqlOffset = "OFFSET @OffsetRows ROWS FETCH NEXT @PageSize ROWS ONLY";
            parameters.Add("@OffsetRows", offsetRows);
            parameters.Add("@PageSize", pageSize);
            #endregion Paging

            #region Sorting
            string sqlOrderBy = ORDERBY_LAST_NAME;
            if(!string.IsNullOrWhiteSpace(sortColumnName))
            {
                // for security reasons to avoid sql injection
                string sortColSql = ORDERBY_LAST_NAME;
                if (ORDERBY_LAST_NAME.Equals(sortColumnName))
                    sortColSql = ORDERBY_LAST_NAME;
                else if (ORDERBY_FIRST_NAME.Equals(sortColumnName))
                    sortColSql = ORDERBY_FIRST_NAME;

                sqlOrderBy = sortColSql + (sortDesc ? " DESC " : " ASC ");
            }

            sqlOrderBy = " ORDER BY " + sqlOrderBy;
            #endregion Sorting

            string sql = string.Format(SQL_FILTER, sqlWhere, sqlOrderBy, EMPLOYEE_COLUMNS, sqlOffset);

            using (var conn = new SqlConnection(_connStr))
            {
                totalCount = conn.ExecuteScalar<int>(sqlCountTotal, parameters);

                return conn.Query<Employee>(sql, parameters);
            }
        }
CONTROLLER

public ActionResult Filter(EmployeeFilterModel filter)
{
            var employees = _repository.Filter(
                filter.FirstName,
                filter.LastName,
                filter.BirthDate,
                out int totalCount,
                filter.SortColumn,
                filter.SortDesc,
                filter.Page);

            filter.Employees = 
  new StaticPagedList<Employee>(employees, filter.Page, 3, totalCount);

            filter.SortDesc = !filter.SortDesc;

            return View(filter);
}

        
    IEnumerable<Employee> GetAll();
    
    Employee GetById(int id);
    
    int Insert(Employee emp);
    
    void Update
        
        
    }
}
