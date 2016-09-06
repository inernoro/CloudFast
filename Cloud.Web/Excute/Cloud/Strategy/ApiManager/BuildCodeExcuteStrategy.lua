
-- 数据库每个对象对应的类型
function getFieldXtype()
    --[[local data = {
        'image','text','uniqueidentifier','tinyint',
        'smallint','int','smalldatetime','real','money',
        'datetime','float','sql_variant','ntext','bit',
        'decimal','numeric','smallmoney','bigint','varbinary',
        'varchar','binary','char','timestamp','sysname','nvarchar','nchar'
    } ]]
    local data = {
        'string','string','string','int',
        'int','int','DateTime','string','string',
        'DateTime','double','string','string','string',
        'decimal','double','string','long','string',
        'string','string','string','long','string','string','string'
    } 
    local key = {
        34,35,36,48,52,56,58,59,60,61,
        62,98,99,104,106,108,122,127,
        165,167,173,175,189,231,231,239
    } 
	local temp = {};
    for k, v in ipairs(key) do
        temp[v] = data[k]
    end
    return temp;
end

-- 执行生成的配置文件
function ExcuteBuild(fields, types)
	
	local modelStr = templateModel(fields,types);
    local tempData = {
        
        model =
        {
            url = "Cloud.Core\\Domain\\@tableName",
            tempList = modelStr
        },
        iRepositories =
        {
            url = "Cloud.Core\\Domain\\I@tableNameRepositories",
            tempList = templateRepositories().IRepositories
        },
        repositories =
        {
            url = "Cloud.Dapper\\Repositories\\@tableNameRepositories",
            tempList = templateRepositories().Repositories
        },
        dto =
        {
            url = "Cloud.Application\\@tableName\\Dtos\\",
            tempList = templateDtos()
        },
        iAppService =
        {
            url = "Cloud.Application\\@tableName\\I@tableNameAppService",
            tempList = templateAppService().IAppService
        },
        appService =
        {
            url = "Cloud.Application\\@tableName\\@tableNameAppService",
            tempList = templateAppService().AppService
        }

    }
    return tempData;
end

-- 生成代码的主方法
function BuildCode( tableName ,fields,types)
--local fields ={ "Id", "Name", "Age", "Item", "Sheck" };
--local types = {34,35,36,48,52,56};
local dictionary = clr.System.Collections.Generic.Dictionary[clr.System.String,clr.System.String]();
   local data = ExcuteBuild(fields , types);
    for k, v in pairs(data) do 
        if (type(v.tempList) == "string") then 
            local key = string.gsub(v.url, "@tableName", tableName);
            local value = string.gsub( v.tempList , "@tableName", tableName );
			dictionary:Add(key, value);
        else
            for k2, v2 in pairs(v.tempList) do
                if(v2~=nil) then 
                    local key2 = string.gsub(v.url..k2, "@tableName", tableName);
                    local value2 = string.gsub(v2, "@tableName", tableName);
				    dictionary:Add(key2, value2);
                end
            end
        end
    end
    return dictionary;
end
 
-- 获取模型方法
function templateModel(fields,types)
    local fx = getFieldXtype();
    local model = "public class @tableName :Entity {\r\n"; 
     for i,v in ipairs(fields) do 
	  model = model .. "\r\n\t" .. "public ".. fx[types[i]].. " " .. v .. "{ get; set; }";
	end
	model = model .. "\r\n}";
    return model;
end

-- 获取仓储
function templateRepositories()
    local templateCode = { };
    -- 仓储接口
    templateCode.IRepositories = [[
                using Cloud.Framework.Dapper;
                namespace Cloud.Domain
                {
            	    public interface I@tableNameRepositories : IDapperRepositories<@tableName>
            	    {
            	    }
                }
            ]];
    -- 仓储实现
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

-- 应用服务层
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
    -- 应用服务接口层
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

-- dto代码
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
                        namespace Cloud.@tableName.Dtos {
                        public class GetOutput {
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

function TesttempList()
	local fields ={ "Id", "Name", "Age", "Item", "Sheck","Ksd" };
	local types = {34,35,36,48,52,56};
	local values = ExcuteBuild(fields,types).model.tempList;
	print(values);
end