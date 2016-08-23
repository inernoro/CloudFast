main = { };
-- startup
function main.start()
    local system = {
        url = "D:/CurrentProject/Cloud4/Cloud.Web/Excute/{0}.lua",
        data =
        {
            infrastructure = "",
            autoConnectionExtend = true
        },
        type = "SystemConfig",
        dataType = "LuaData",
        contentType = "Cloud.Strategy.LuaHelper"
    }
    return system;
end



-- print(main.start().infrastructure.cache().url);
function main.getBase(key, parent)

    return main.start().infrastructure[key]()[parent];

end