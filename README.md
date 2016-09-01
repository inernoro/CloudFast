# CloudFast

> 架构组件：<br/>

DDD + TDD + BDD<br/>

> 数据库：<br/>

sqlserver 2012 + redis3.0 + mongodb 3.0 <br/>

> 核心组件（第一排为未来的新组件）：<br/>

MVC5/Nancy + Abp/Enode + .netCore 1.0 + HangFire + OData+SignalR + Equeue +cloudEvent  <br/>
DynamicWebApi+ solr +StackExchange.Redis + dapper + Mongo.Drive +  Owin+ AutoMapper  <br/>
Castle.Windsor + Modernizr +Shouldly + Log4Net + Angularjs<br/>

> 特色：

1. 前端使用vue jquery开发 ，扩展和更改都非常清晰和方便。
2. 加载速度快，即时相应，轻量级的框架，下载页面到渲染都是毫秒级。
3. 自动日期，数值，bool，string，电话类型分辨，提交表单的时候就能校验，省去查看代码input的时间。
4. get post状态自动分辨，不用手动切换，自动变更为设置api时的状态
5. 跨域提交，无需担心跨域带来的接口调用烦恼，节省宝贵调试时间
6. lua脚本化配置，可以放在lua.e2e100.com调试，不用看冗余的xml和config，支持lua脚本编程，程序集调用，传入参数和结果支持计算，可调式，可配置，真正做到ai级的控制。
7. 事务自动调用lua触发事件实现缓存联动和未来solr更新，给予事件分离与程序集未来实现cqrs非常方便，只变更lua脚本就可实现服务器策略变更位置，事务ai级控制。
8. 搜索时自动判断搜索内容，如果搜索的是大标题的内容则保留所有子项，若搜索到子项，则自动筛选其他子项
9. bootstrap响应式设计，自动适配移动端，不用担心移动端样式问题，一台完成，多台使用。

＃下面是本程序集用到的框架，和框架说明

> abp核心特色

用户&角色管理<br/>
系统设置存取管理（系统级、租户级、用户级，作用范围自动管理）<br/>
审计日志（自动记录每一次接口的调用者和参数）<br/> 

> Abp简介

1. 基础框架组件独立、通用，可用于多个不同项目。类似于daxnet的Apworks框架。<br/>
2. 对项目实现模块化开发提供了支持，每个模块有独立的EF DbContext，可单独指定数据库。<br/>
3. 对DDD的技术实现进行了封装，让项目以极精简的代码，专注于业务领域。<br/>
4. 多租户支持，每个租户的数据自动隔离，业务模块开发者不需要手动操作TenantId。<br/>
5. 集成ASP.NET Identity，实现登录认证、功能权限授权&验证、角色和用户管理。<br/>
6. 集成Log4Net，实现日志记录。<br/>
7. 集成AutoMapper，实现Dto类与实体类的双向自动转换。<br/>
8. 实现UnitOfWork模式，为应用层和仓储层的(会写数据库的)方法自动实现数据库事务。<br/>
9. 可通过ApplicationService的方法自动建立相应的WebApi方法，ajax可直接调用，不需要写ApiController和Action。<br/>
10. 调用ApplicationService的方法时，自动验证权限和参数有效性(用相应的Attribute标注)。<br/>
11. 继承自FullAuditedEntity基类的领域实体，会自动实现软删除（在数据库中用IsDeleted字段进行标注）。<br/>
12. 实现一系列扩展方法，简化编码。<br/>
<br/>

> 架构图 

![image](http://images0.cnblogs.com/blog2015/534304/201505/272237582662256.png)<br/>

本框架扩展于abp,若想访问abp,框架原地址[点击此处](https://github.com/aspnetboilerplate/aspnetboilerplate)
