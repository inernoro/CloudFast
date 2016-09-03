local temp = { };

-- 数据库每个对象对应的类型
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


function ExcuteBuild(tableName, fields)
    local xtype = getFieldXtype();
    local tempData = {
        dto =
        {
            url = "/@tableName/Dtos/@selfName",
            tempList = templateDtos()
        },

        appService =
        {
            url = "/@tableName/@tableNameAppServices",
            tempList = templateAppService().AppService
        },
        model =
        {
            url = "/Domain/@tableName",
            tempList = templateModel(fields)
        },

        repositories =
        {
            url = "/Repositories/@tableNameRepositories",
            tempList = templateRepositories().Repositories
        },
        iAppService =
        {
            url = "/@tableName/I@selfNameAppServices",
            tempList = templateRepositories().IRepositories
        }

    }
    return tempData;
end

function build( )
    local  data = ExcuteBuild("temp",{"Id","name"});
    for k,v in pairs(data) do
        if(type(v.tempList)=="string") then 
            print(string.gsub(v.tempList, "@tableName", "tempEntity"),"")
        else
            for k2,v2 in pairs(v.tempList) do
                print(string.gsub(v2, "@tableName", "tempEntity"),"")
            end
        end
    end

end
 
--获取模型方法
function templateModel(fields)
    local  model = [[
public class @tableName :Entity
{ ]];
     for k,v in ipairs(fields) do
		model = model .. "\r\n\t" .. "public string " .. v .. "{ get; set; }";
	end
    model = model .. "\r\n}"
    return model;
end

--print(templateModel({"item","buide"}));

--获取仓储
function templateRepositories()
    local templateCode = { };

    templateCode.IRepositories = [[
            using Cloud.Framework.Dapper;
            namespace Cloud.Domain
            {
        	    public interface I@tableNameRepositories : IDapperRepositories<@tableName>
        	    {
        	    }
            }
        ]];

    templateCode.Repositories = [[
        using Cloud.Domain;
        namespace Cloud.Dapper.Framework
        {
        	public class @tableNameRepositorie : DapperRepositories<@tableName>, I@tableNameRepositories
        	{
        	}
        }
        ]];

    return templateCode;
end

function templateAppService()

    local templateCode = { };

    templateCode.AppService = [[
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using Abp.AutoMapper;
        using Abp.UI;
        using Cloud.Domain;
        using Cloud.Framework;
        using Cloud.@tableName.Dtos;
        namespace Cloud.@tableName
        {
            public class @tableNameAppService : CloudAppServiceBase, I@tableNameAppService
            {
                private readonly I@tableNameRepositorie _@tableNameRepositorie;
                public @tableNameAppService(I@tableNameRepositorie @tableNameRepositorie)
                {
                    _@tableNameRepositorie = @tableNameRepositorie;
                }
                public Task Post(PostInput input)
                {
                    var model = input.MapTo<Domain.@tableName>();
                    return _@tableNameRepositorie.InsertAsync(model);
                }
                public Task Delete(DeletetInput input)
                {
                    return _@tableNameRepositorie.DeleteAsync(input.Id);
                }
                public Task Put(PutInput input)
                {
                    var oldData = _@tableNameRepositorie.Get(input.Id);
                    if (oldData == null)
                        throw new UserFriendlyException("该数据为空，不能修改");
                    var newData = input.MapTo(oldData);
                    return _@tableNameRepositorie.UpdateAsync(newData);
                }
                public Task<GetOutput> Get(GetInput input)
                {
                    return Task.Run(() => _@tableNameRepositorie.Get(input.Id).MapTo<GetOutput>());
                }
                public async Task<GetAllOutput> GetAll(GetAllInput input)
                {
                    var page = await Task.Run(() => _@tableNameRepositorie.ToPaging("@tableName", input, "*", "Id", new { }));
                    return new GetAllOutput() { Items = page.MapTo<IEnumerable<@tableNameDto>>() };
                }
            }
        }
                ]]

    templateCode.IAppService = [[
        using System.Threading.Tasks;
        using Abp.Application.Services;
        using Cloud.Framework.Assembly;
        using Cloud.@tableName.Dtos;
        namespace Cloud.@tableName
        {
            public interface I@tableNameAppService : IApplicationService
            {
                [ContentDisplay("添加")]
                Task Post(PostInput input);
                [ContentDisplay("删除")]
                Task Delete(DeletetInput input);
                [ContentDisplay("修改")]
                Task Put(PutInput input);
                [ContentDisplay("获取")]
                Task<GetOutput> Get(GetInput input);
                [ContentDisplay("获取多条")]
                Task<GetAllOutput> GetAll(GetAllInput input);
            }
        }
                ]]

    return templateCode;
end

-- 模板文件
function templateDtos()

    local templateCode = { };

    templateCode.DeleteInput = [[
                    namespace Cloud.@tableName.Dtos
                {
                    public class DeletetInput
                {
                    public int Id { get; set; }
                    }
                    }
                    ]];
    templateCode.GetAllInput = [[
                    using Cloud.Framework;
    namespace Cloud.@tableName.Dtos
                {
                    public class GetAllInput : PageIndex
                {
                    }
                    }
                    ]];
    templateCode.GetAllOutput = [[
    using System.Collections.Generic;
    namespace Cloud.@tableName.Dtos
                {
                    public class GetAllOutput
                {
                    public IEnumerable<@tableNameDto> Items { get; set; }
                    }
                    }
                    ]];
    templateCode.GetInput = [[
                    namespace Cloud.@tableName.Dtos
                {
                    public class GetInput
                {
                    public int Id { get; set; }
                    }
                    }
                    ]];

    templateCode.GetOutput = [[
                    namespace Cloud.@tableName.Dtos
                {
                    public class GetOutput
                {
                    }
                    }
                    ]];
    templateCode.PostInput = [[
    using Abp.AutoMapper;
    namespace Cloud.@tableName.Dtos
                {
                    [AutoMap(typeof(Domain.@tableName))]
                    public class PostInput
                {
                    }
                    }
                    ]];
    templateCode.PutInput = [[
                    using Abp.AutoMapper;
    namespace Cloud.@tableName.Dtos
                {
                    [AutoMap(typeof(Domain.@tableName))]
                    public class PutInput
                {
                    public int Id { get; set; }
                    }
                    }
                    ]];
    templateCode.TemplateDto = [[
                    using Abp.AutoMapper;
    namespace Cloud.@tableName.Dtos
                {
                    [AutoMap(typeof(Domain.@tableName))]
                    public class @tableNameDto
                {
                    }
                    }
                    ]];
    return templateCode;
end



build();
