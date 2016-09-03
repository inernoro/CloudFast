
function BuildAllCode()
    local sql = {
        tables = "SELECT Name,crdate as CreateTime FROM sys.SysObjects Where XType='U' ORDER BY crdate desc",
        field = " "
    };
end

function BuildCode()
    local sql = {
        tables = "SELECT Name FROM sys.SysObjects Where XType='U' ORDER BY Name ",
        field = "SELECT Name FROM SysColumns WHERE id=Object_Id('{0}')"
    };
end