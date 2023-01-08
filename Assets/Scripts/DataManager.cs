using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class DataManager
{
    public static SetupAttributeCollection setupAttributes;

    public static void Init()
    {
        DataTable dt = SqlliteDbConnector.GetSelectResultToDataTable("SELECT * FROM TB_SETUP");
        setupAttributes = ConvertDataTableToSetupAttributes(table: dt);
    }

    private static SetupAttributeCollection ConvertDataTableToSetupAttributes(DataTable table)
    {
        SetupAttributeCollection attributes = new SetupAttributeCollection();

        DataRow row;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            row = table.Rows[i];

            if (int.TryParse(s: row[ColumnNames.ID].ToString(), result: out int id))
            {
                attributes.Add(new SetupAttribute(
                    id: id,
                    group: row[ColumnNames.GROUP].ToString(),
                    name: row[ColumnNames.NAME].ToString(),
                    value: row[ColumnNames.VALUE].ToString(),
                    desc: row[ColumnNames.DESC].ToString()
                ));
            }
        }

        return attributes;
    }
}
