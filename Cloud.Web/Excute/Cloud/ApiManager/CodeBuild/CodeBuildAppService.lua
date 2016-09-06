
function BuildAllCode()
    local sql = {
        tables = [[SELECT so.Name,sc.system_type_id as xtype,sc.name as colName,crdate as CreateTime FROM sys.SysObjects so
inner join sys.columns sc on sc.object_id = object_id( so.name)
 Where XType='U' ORDER BY crdate desc]],
        field = " "
    };
    return sql;
end

function BuilDictionary()
    local sql = {
        tables = [[SELECT so.Name,sc.system_type_id as xtype,sc.name as colName,crdate as CreateTime FROM sys.SysObjects so
inner join sys.columns sc on sc.object_id = object_id(so.name)
 Where XType='U' and so.name = @name ORDER BY crdate desc]]
    };
    return sql;
end