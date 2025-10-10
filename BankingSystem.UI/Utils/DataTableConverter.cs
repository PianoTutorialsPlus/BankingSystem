using System.Data;

namespace BankingSystem.UI.Utils;

public static class DataTableConverter
{
    public static DataTable ToDataTable<T>(IEnumerable<T> items)
    {
        var dt = new DataTable();
        var props = typeof(T).GetProperties();
        foreach (var p in props) dt.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
        foreach (var item in items)
        {
            var row = dt.NewRow();
            foreach (var p in props) row[p.Name] = p.GetValue(item) ?? DBNull.Value;
            dt.Rows.Add(row);
        }
        return dt;
    }
}
