
function BuildAllCode()
    local sql = {
        tables = [[SELECT so.Name,sc.system_type_id as xtype,sc.name as colName,crdate as CreateTime FROM sys.SysObjects so
inner join sys.columns sc on sc.object_id = object_id( so.name)
 Where XType='U' ORDER BY crdate desc]],
        field = " "
    };
    return sql;
end

function BuildCode()
    local sql = {
        tables = "SELECT Name FROM sys.SysObjects Where XType='U' ORDER BY Name ",
        field = "SELECT Name FROM SysColumns WHERE id=Object_Id('{0}')"
    };
end