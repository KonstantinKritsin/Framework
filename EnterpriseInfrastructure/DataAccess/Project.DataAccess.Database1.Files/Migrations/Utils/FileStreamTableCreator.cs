namespace Project.DataAccess.Database1.Files.Migrations.Utils
{
    public static class FileStreamTableCreator
    {
        public static string PrepareDbSql = @"
exec('alter database [Project.Files] add filegroup FileStreamDataGroup contains filestream');
declare @Default_Data_Path varchar(512);
select @Default_Data_Path=(
select left(physical_name,len(physical_name)-charindex('\',reverse(physical_name))+1) 
from sys.master_files mf
join sys.[databases] d
on mf.[database_id] = d.[database_id]
where d.[name] = 'Project.Files' and type = 0);
exec('alter database [Project.Files] add file (NAME=FileStreamDataGroupFile,FILENAME=''' + @Default_Data_Path + 'Project.Files'') to filegroup FileStreamDataGroup')";

        public static string TableSql = @"
create table dbo.FileStreamData
(
[Id] bigint primary key clustered not null,
[RowId] uniqueidentifier rowguidcol unique nonclustered not null default(newid()),
[Data] varbinary(max) filestream not null default(0x),
[Path] as ([Data].PathName()),
[Transaction] as (GET_FILESTREAM_TRANSACTION_CONTEXT())
)";
    }
}