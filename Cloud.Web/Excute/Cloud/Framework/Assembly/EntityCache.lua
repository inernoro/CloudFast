function EntityCreatedEventData(model)
    -- return clr.Cloud.Framework.Assembly.Cache.Call(model);
    return clr.Cloud.Framework.Assembly.Cache2.Call(model);
end

function EntityChangedEventData(model)
    -- return clr.Cloud.Framework.Assembly.Cache.Call(model);
    return clr.Cloud.Framework.Assembly.Cache2.Call(model);
end

function EntityDeletedEventData(model)
    -- return clr.Cloud.Framework.Assembly.Cache.Call(model);
    return clr.Cloud.Framework.Assembly.Cache2.Call(model);
end