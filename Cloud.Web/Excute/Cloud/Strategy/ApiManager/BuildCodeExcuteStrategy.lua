local temp = { };

--数据库每个对象对应的类型
function getFieldXtype()
    if (temp ~= nil) then
        return temp;
    end
    local data = {
        'image','text','uniqueidentifier','tinyint',
        'smallint','int','smalldatetime','real','money',
        'datetime','float','sql_variant','ntext','bit',
        'decimal','numeric','smallmoney','bigint','varbinary',
        'varchar','binary','char','timestamp','sysname','nvarchar','nchar'
    }
    local key = {
        34,35,36,48,52,56,58,59,60,61,
        62,98,99,104,106,108,122,127,
        165,167,173,175,189,231,231,239
    }
    local temp = { };
    for k, v in ipairs(key) do
        temp[v] = data[k]
    end
end


function ExcuteBuild(tableName,fields)
    local xtype = getFieldXtype();


end

--模板文件
function template( )
    local temp = [[
    
    
]]


end