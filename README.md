
## General info
Hi!<br>
The project was created to check possibilities of [PostgreSQL](https://www.postgresql.org/) and [Materialize](https://materializecss.com/) used in the ASP.NET framework. [Entity Framework NPGSQL ](https://www.npgsql.org/ef6/) has been added to the project to connect to the Postgres database to use stored procedures, functions and tables. <br><br>
I chosen subject of car parts to work with real items, problems an easier-to-imagine website. Also I can change subject on another for example clothes. <br><br>

![rimshop](https://user-images.githubusercontent.com/29818201/133937342-834dc833-e90e-4c33-99c4-16e75e54af2b.PNG)

## Features
- [x] Form for submitting product offers.
- [x] Module displaying the list of products.
- [x] Management management module.
- [x] User rights structure.
- [x] Automatically generating tabs like "Rims", "Wheel covers" from the database according to the category and user permissions.
- [x] Login.
- [x] User panel.

## Technologies
Project is created with:
* ASP.NET WebForms
* PostgreSQL 13
* Entity Framework 6.4.4
* EntityFramework6.Npgsql
* Materialize 1.0.0
* jQuery 3.6.0
* NUnit 3.13.2
* TinyMCE

<br><br>
## Comparison with the use of PostgreSQL and Microsoft SQL Express in ASP.NET technology.
| |PostgreSQL |MicrosofSQL Express |
|--|---------|-------|
| *License* | Free and open source| SQL Server Express may only be used at no charge for development and testing, as well as for “micro workloads” such as mobile or web apps with minimal relational database requirements. The free license does not apply to instances running on virtual machines.|
| *Entity Framework procedures and functions* | Impossible to import stored procedures or functions by Entity | Possible to import stored procedures or functions by EntityFramework|
| *Entity Framework installation* | A lot of issues with install and connect  | Simple and easy using EntityFramework. No problems with connection with DB, importing tables, functions etc. |
| *Tools for DB management* | pgAdmin opening in web browser and has limitation of queries in new window. Inconvinient interface. | Microsoft SQL Management Studio advanced tool and user friendly interface |

## GIF Presentation
<br>![rimshop_1](https://user-images.githubusercontent.com/29818201/134059353-1bf84494-be94-47a8-9468-5bff030be78d.gif)

## Tutorial of using stored procedures of PostgreSQL with EntityFramework at the ASP.NET application.
Creating stored procedures with parameters returning and using them in a .NET project from EntityFramework was a small challenge. I tried to find documentation on google, but there is less information about PostgreeSQL than eg Microsoft SQL. By trial and error, it was successful. I want to share it below. . <br><br>

1. Install Entity Framework and connect to DB. There is some excellent tutorials so I'll not describe this. [Google - using Entity Framework with PostgreeSQL](https://www.google.com/search?q=postgresql+visual+studio+entity&ei=FMtIYbfcFsOQrgS7u4WoDw&oq=postgree+sql+visual+studio+entity&gs_lcp=Cgdnd3Mtd2l6EAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsAMyBwgAEEcQsANKBAhBGABQAFgAYIQkaABwA3gAgAEAiAEAkgEAmAEAyAEIwAEB&sclient=gws-wiz&ved=0ahUKEwi3tv7lj47zAhVDiIsKHbtdAfUQ4dUDCA4&uact=5). <br>

2. Add folder Beens and class with properties Result_Been.cs inside.<br>We will use this to return result of completed method AddLog.

```csharp
    public class Result_Been
    {
        public Boolean IsError { get; set; }

        public string Message { get; set; }


        public int? Id { get; set; }
    }
```

3. Add DBBase.cs class to project. At content you can find creating parameters used to run stored procedure or function. <br> 
```csharp
public class DBBase
    {
        public NpgsqlParameter CreateNpgsqlParameter(string name, NpgsqlDbType type, object value, System.Data.ParameterDirection direction = 		System.Data.ParameterDirection.Input)
        {
            var _parameter = new NpgsqlParameter(name, type);
            _parameter.Direction = direction;
            _parameter.Value = value;

            return _parameter;
        }

        public ObjectParameter CreateObjectParameter(string name, Type type, object value)
        {
            var param = value != null ?
               new ObjectParameter(name, value) :
               new ObjectParameter(name, type);
            
            return param;
        }
    }
```
 <br>
4. The next step is add class DbStoredProcedure.cs that inerhit from DBBase. It is created with design pattern Singleton. <br>
I attached procedure to save log in the database for this example. 

```csharp
  public class DbStoredProcedure : DBBase
    {
        public static DbStoredProcedure _object;

        public DbStoredProcedure() { }

        public enum LogType
        {
            Error = 1,
            Warning = 2,
            Information = 3
        }

        public static DbStoredProcedure Instance()
        {
            if (_object == null)
            {
                _object = new DbStoredProcedure();
            }
            return _object;
        }


       	public Beens.Result_Been AddLog(int? idUser, LogType logType, String module, String description)
        {
            try
            {
                using (var dbo = new ourShopEntities())
                {
                    var _iduser = CreateNpgsqlParameter("_iduser", NpgsqlDbType.Integer, idUser);
                    var _logType = CreateNpgsqlParameter("_idlogstype", NpgsqlDbType.Integer, (int)logType);
                    var _hostname = CreateNpgsqlParameter("_hostname", NpgsqlDbType.Varchar, "");
                    var _module = CreateNpgsqlParameter("_module", NpgsqlDbType.Varchar, module);
                    var _description = CreateNpgsqlParameter("_description", NpgsqlDbType.Varchar, description);

                    dbo.Database.ExecuteSqlCommand("call public.add_log(@_iduser, @_idlogstype, @_hostname, @_module, @_description);", _iduser, _logType, _hostname, _module, _description);
                    return new Beens.Result_Been { IsError = false, Message = "Added log successfully." };
                }
            }
            catch(Exception ex)
            {
                return new Beens.Result_Been { IsError = true, Message = ex.Message };
            }
        }
}
```

5. This step is to do in the Postgres DB. Create tables and stored procedure like below.
```sql

CREATE TABLE public."LogsType"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" character varying(200) COLLATE pg_catalog."default",
    CONSTRAINT "LogsType_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."LogsType"
    OWNER to postgres;
```
```sql
CREATE TABLE public."Log"
(
    "CreationDate" timestamp without time zone NOT NULL,
    "IdLogsType" integer,
    "IdUser" integer,
    "HostName" character varying(200) COLLATE pg_catalog."default",
    "Module" character varying(200) COLLATE pg_catalog."default",
    "Description" character varying(500) COLLATE pg_catalog."default"
)

TABLESPACE pg_default;

ALTER TABLE public."Log"
    OWNER to postgres;
```
```sql
INSERT INTO public."LogsType"(
	 "Name")
VALUES ('Error');

INSERT INTO public."LogsType"(
	 "Name")
VALUES ('Warning');

INSERT INTO public."LogsType"(
	 "Name")
VALUES ('Information');
```
```sql
CREATE OR REPLACE PROCEDURE public.add_log(
	_iduser integer DEFAULT NULL::integer,
	_idlogstype integer DEFAULT NULL::integer,
	_hostname character varying DEFAULT NULL::character varying,
	_module character varying DEFAULT NULL::character varying,
	_description character varying DEFAULT NULL::character varying)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
BEGIN
	INSERT INTO public."Log"(
	"CreationDate", "IdLogsType", "IdUser", "HostName", "Module", "Description")
	VALUES (NOW(), _idlogstype, _iduser, _hostname, _module, _description);
END
$BODY$;

GRANT EXECUTE ON PROCEDURE public.add_log(integer, integer, character varying, character varying, character varying) TO postgres;
```

6. Add nUnit Framework to your NuGet packages.
7. Create unit test for check saving logs by application. Add to your project class DbTest.cs
```csharp
 [TestFixture]
    public class DBTest
    {
        [Test]
        public void SaveLogTest()
        {
            testLog(DbStoredProcedure.LogType.Error);
            testLog(DbStoredProcedure.LogType.Information);
            testLog(DbStoredProcedure.LogType.Warning);
        }

        private void testLog(DbStoredProcedure.LogType logType)
        {
            var ret = DbStoredProcedure.Instance().AddLog(-1, logType, "TEST", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.IsFalse(ret.IsError);
        }        
}
```

8. Rebuild soultion and run test explorer in Visual Studio.
9. Run test.

![image](https://user-images.githubusercontent.com/29818201/134058804-fa6b6f50-9674-4688-9c82-95e632457eaf.png)

10 
![image](https://user-images.githubusercontent.com/29818201/134058191-80618f09-579c-4691-85d1-5762da3fd7f8.png)

11. Results in the database.
![image](https://user-images.githubusercontent.com/29818201/134057964-94f3f5d8-ba42-4f6b-866b-3b3c4dc11bd2.png)

PostgreSQL offer free solution..
