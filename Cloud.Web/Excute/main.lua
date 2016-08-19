local main = { };
local dataConfig = require 'DataConfig'

-- startup
function main.start()
    local system = {
        url = "F:\\wwwroot\\Luafile\\{0}.lua",
        data =
        {
            infrastructure = dataConfig,
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