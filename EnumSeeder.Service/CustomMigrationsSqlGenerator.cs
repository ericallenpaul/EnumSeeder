using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EnumSeeder.Service
{
    public class CustomMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        public CustomMigrationsSqlGenerator(
            MigrationsSqlGeneratorDependencies dependencies,
            IMigrationsAnnotationProvider migrationsAnnotations)
            : base(dependencies, migrationsAnnotations)
        {
        }

        protected override void Generate(
            MigrationOperation operation,
            IModel model,
            MigrationCommandListBuilder builder)
        {
            if (operation is CreateEnumRowOperation createEnumRowOperation)
            {
                Generate(createEnumRowOperation, builder);
            }
            else
            {
                base.Generate(operation, model, builder);
            }
        }

        private void Generate(
            CreateEnumRowOperation operation,
            MigrationCommandListBuilder builder)
        {
            var sqlHelper = Dependencies.SqlGenerationHelper;
            var stringMapping = Dependencies.TypeMappingSource.FindMapping(typeof(string));

            builder
                .Append($"If NOT EXISTS(SELECT Id FROM {operation.TableName} WHERE Id = {stringMapping.GenerateSqlLiteral((operation.Id))})")
                .Append("BEGIN")
                .Append($"INSERT INTO {sqlHelper.DelimitIdentifier(operation.TableName)}")
                .Append("(")
                .Append("")
                .Append("[Id]")
                .Append(",[Name]")
                .Append(",[Description]")
                .Append(")")
                .Append("VALUES")
                .Append("(")
                .Append($"{stringMapping.GenerateSqlLiteral((operation.Id))}")
                .Append("")
                .Append($",'{stringMapping.GenerateSqlLiteral((operation.Name))}'")
                .Append($",'{stringMapping.GenerateSqlLiteral((operation.Description))}'")
                .Append(")")
                .Append("END")
                .AppendLine(sqlHelper.StatementTerminator)
                .EndCommand();
        }
    }
}
